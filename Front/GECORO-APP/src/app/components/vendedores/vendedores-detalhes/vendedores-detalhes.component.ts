import { RegraVendedor } from './../../../models/RegraVendedor';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Vendedor } from 'src/app/models/Vendedor';
import { VendedorService } from 'src/app/services/vendedor.service';

@Component({
  selector: 'app-vendedores-detalhes',
  templateUrl: './vendedores-detalhes.component.html',
  styleUrls: ['./vendedores-detalhes.component.css']
})
export class VendedoresDetalhesComponent implements OnInit {
  form: FormGroup = new FormGroup({});
  regraVendedorForm: FormGroup = new FormGroup({});
  vendedor = {} as Vendedor
  regraVendedor = {} as RegraVendedor

  get f(): any{
    return this.form.controls;
  }

  constructor(private fb: FormBuilder,
    private vendedorService: VendedorService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private activeRoute: ActivatedRoute,
    private router: Router) {
   }

  ngOnInit() {
    this.carregarVendedor();
    this.validation();
  }

  private validation(): void{
    this.form = this.fb.group({
      nome: ['',Validators.required],
      codigo: ['',[Validators.required, Validators.minLength(5), Validators.maxLength(5),
                Validators.pattern("[0-9]{5}")]],
        regraVendedorForm: this.fb.group({
        parcelasPagas: ['', Validators.required],
        saldoDevedor: ['', Validators.required]
      })
    });

  }

  public carregarVendedor(): void{
    const vendedorId = this.activeRoute.snapshot.paramMap.get("id");
      if(vendedorId != null){
        this.vendedorService.getVendedorById(+vendedorId).subscribe(
          (vendedor: Vendedor) => {
            this.vendedor = {... vendedor};
            this.form.patchValue(this.vendedor);
          },
          (error: any) =>{
            console.log(error);
          }
        ).add(() => this.spinner.hide());
      }
  }

  public SalvarAlteracoes(): void{
    if(this.form.valid){

      this.vendedor = {... this.form.value}
      this.regraVendedor.parcelasPagas = this.regraVendedorForm.value['parcelasPagas'];
      this.regraVendedor.saldoDevedor = this.regraVendedorForm.value['saldoDevedor'];
      this.vendedor.regraVendedor = this.regraVendedor;


      this.vendedorService.post(this.vendedor).subscribe(
        () => this.toastr.success('Vendedor salvo com sucesso!', 'Sucesso'),
        (error: any) => {
          console.log(error);
          this.toastr.error('Erro ao salvar vendedor!', 'Erro');
        }
      ).add(() => this.spinner.hide());
    }
  }
}
