import { Observable } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { ClienteService } from 'src/app/services/cliente.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalService } from 'ngx-bootstrap/modal';
import { Cliente } from 'src/app/models/Cliente';

@Component({
  selector: 'app-clientes-detalhes',
  templateUrl: './clientes-detalhes.component.html',
  styleUrls: ['./clientes-detalhes.component.css']
})
export class ClientesDetalhesComponent implements OnInit {

  form: FormGroup = new FormGroup({});
  cliente = {} as Cliente
  constructor(private fb: FormBuilder,
    private clienteService: ClienteService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private activeRoute: ActivatedRoute,
    private router: Router) { }

  ngOnInit() {
    this.carregarCliente();
    this.validation();
  }

  get f(): any{
    return this.form.controls;
  }

  public validation(): void{
    this.form = this.fb.group({
      nome: ['',Validators.required],
      cpf: ['',[Validators.required, Validators.minLength(11), Validators.maxLength(11),
                Validators.pattern("[0-9]{11}")]]
    })
  }

  public carregarCliente(): void{
    const clienteId = this.activeRoute.snapshot.paramMap.get("id");
      if(clienteId != null){
        this.clienteService.getClienteById(+clienteId).subscribe(
          (cliente: Cliente) => {
            this.cliente = {... cliente};
            this.form.patchValue(this.cliente);
          },
          (error: any) =>{
            console.log(error);
          }
        ).add(() => this.spinner.hide());
      }
  }

  public SalvarAlteracoes(): void{
    if(this.form.valid){

      this.cliente = {... this.form.value}

      this.clienteService.post(this.cliente).subscribe(
        () => this.toastr.success('Cliente salvo com sucesso!', 'Sucesso'),
        (error: any) => {
          console.log(error);
          this.toastr.error('Erro ao salvar cliente!', 'Erro');
        }
      ).add(() => this.spinner.hide());
    }
  }
}
