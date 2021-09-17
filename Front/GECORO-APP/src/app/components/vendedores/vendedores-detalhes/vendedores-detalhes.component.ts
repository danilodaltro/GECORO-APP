import { RegraVendedor } from './../../../models/RegraVendedor';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
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
  vendedor = {} as Vendedor;
  regraVendedor = {} as RegraVendedor;
  estadoSalvar: string = 'post';
  _parcelasPagas: any = '';
  _saldoDevedor: any = '';
  get f(): any{
    return this.form.controls;
  }

  constructor(private fb: FormBuilder,
    private vendedorService: VendedorService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private activeRoute: ActivatedRoute) {

   }

   public get parcelasPagas(){
    return this._parcelasPagas;
   }

   public set parcelasPagas(value: number){
     this._parcelasPagas = value;
   }

   public get saldoDevedor(){
    return this._saldoDevedor;
  }

   public set saldoDevedor(value: number){
    this._saldoDevedor = value;
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
      parcelas: ['', [Validators.required, Validators.pattern("[0-9]+")]],
      saldoDev: ['', [Validators.required,Validators.pattern("[0-9]+\.[0-9]{2}")]]
    });
  }

  public carregarVendedor(): void{
    const vendedorId = this.activeRoute.snapshot.paramMap.get("id");
      if(vendedorId != null){
        this.vendedorService.getVendedorById(+vendedorId).subscribe(
          (vendedor: Vendedor) => {
            this.vendedor = {... vendedor};
            this.form.patchValue(this.vendedor);
            if(Object.values(this.vendedor.regraVendedor).length > 0){
              this._parcelasPagas = vendedor.regraVendedor.parcelasPagas;
              this._saldoDevedor = vendedor.regraVendedor.saldoDevedor;
              this.regraVendedor = this.vendedor.regraVendedor;
            }
            this.estadoSalvar = 'put';
          },
          (error: any) =>{
            console.log(error);
          }
        ).add(() => this.spinner.hide());
      }
  }

  public SalvarAlteracoes(): void{
    if(this.form.valid){
      this.vendedor = this.estadoSalvar == 'post'?
                     {... this.form.value}:
                     {id: this.vendedor.id, ... this.form.value};

      this.regraVendedor.parcelasPagas = this._parcelasPagas;
      this.regraVendedor.saldoDevedor = this._saldoDevedor;
      this.vendedor.regraVendedor = this.regraVendedor;

      this.vendedorService[this.estadoSalvar == 'post' ? 'post' : 'put'](this.vendedor).subscribe(
        () => this.toastr.success('Vendedor salvo com sucesso!', 'Sucesso'),
        (error: any) => {
          console.log(error);
          this.toastr.error('Erro ao salvar vendedor!', 'Erro');
        }
      ).add(() => this.spinner.hide());
    }
  }
}
