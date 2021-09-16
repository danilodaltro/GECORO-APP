import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { ClienteService } from 'src/app/services/cliente.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute } from '@angular/router';
import { Cliente } from 'src/app/models/Cliente';
import { Vendedor } from 'src/app/models/Vendedor';
import { VendedorService } from 'src/app/services/vendedor.service';
@Component({
  selector: 'app-clientes-detalhes',
  templateUrl: './clientes-detalhes.component.html',
  styleUrls: ['./clientes-detalhes.component.css']
})
export class ClientesDetalhesComponent implements OnInit {

  form: FormGroup = new FormGroup({});
  cliente = {} as Cliente;
  vendedores = [] as Vendedor[];
  vendedorCliente = {} as Vendedor
  estadoSalvar: string = 'post';
  toggleFlag: boolean = false;
  vendedorClienteRequired: boolean = false;

  constructor(private fb: FormBuilder,
    private clienteService: ClienteService,
    private vendedorService: VendedorService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private activeRoute: ActivatedRoute) {}

  ngOnInit() {
    this.spinner.show();
    this.carregarVendedores();
    this.carregarCliente();
    this.validation();
    this.spinner.hide();
  }

  public carregarVendedores(): void {
    this.vendedorService.getVendedores().subscribe(
      (vendedores: Vendedor[]) =>
      {
        this.vendedores = vendedores;
      },
      (error: any) =>
      {
        console.log(error);
        this.toastr.error(error,"Erro ao carregar vendedores!");
      }
    );
  }

  get f(): any{
    return this.form.controls;
  }

  public set vendedorSelecionado(value: any){
    this.vendedorCliente = value;
  }

  public get vendedorSelecionado(){
    return this.vendedorCliente;
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
            if(this.cliente.vendedorId == 0){
              this.vendedorCliente.nome = '';
            }
            else{
              this.vendedorCliente = this.cliente.vendedor;
            }
            this.vendedorSelecionado = this.vendedorCliente
            this.estadoSalvar = 'put';
            this.vendedorClienteRequired = true;
          },
          (error: any) =>{
            console.log(error);
          }
        ).add(() => this.spinner.hide());
      }
  }

  public alteraVendedorSelecionado(vend: Vendedor): void{
    this.vendedorCliente = vend;
    this.vendedorClienteRequired = true;
    this.showDropdown();
  }

  public SalvarAlteracoes(): void{
    if(this.form.valid){
      this.cliente = this.estadoSalvar == 'post'?
                     {... this.form.value}:
                     {id: this.cliente.id, ... this.form.value};

      this.cliente.vendedorId = this.vendedorSelecionado.id;

      this.clienteService[this.estadoSalvar == 'post' ? 'post' : 'put'](this.cliente).subscribe(
        () => this.toastr.success('Cliente salvo com sucesso!', 'Sucesso'),
        (error: any) => {
          console.log(error);
          this.toastr.error('Erro ao salvar cliente!', 'Erro');
        }
      ).add(() => this.spinner.hide());
    }
  }

  public showDropdown() { this.toggleFlag = !this.toggleFlag; }
}
