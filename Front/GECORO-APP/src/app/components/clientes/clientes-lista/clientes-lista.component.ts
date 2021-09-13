import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Cliente } from 'src/app/models/Cliente';
import { ClienteService } from 'src/app/services/cliente.service';

@Component({
  selector: 'app-clientes-lista',
  templateUrl: './clientes-lista.component.html',
  styleUrls: ['./clientes-lista.component.css']
})
export class ClientesListaComponent implements OnInit {

  modalRef?: BsModalRef;
  public clientes: Cliente[] = [];
  public clientesFiltrados: Cliente[] = [];
  public _filtroLista: string = "";
  public nomeBtnFiltro: string = "CPF";
  constructor(
    private clienteService: ClienteService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router,
    private activeRoute: ActivatedRoute
  ) { }

  ngOnInit() {
    this.spinner.show();
    if(this.router.url.includes("vendedor")){
      const vendedorId = this.activeRoute.snapshot.paramMap.get("id");
      if(vendedorId != null){
        this.getClientesByVendedor(+vendedorId);
      }
    }
    else this.getClientes();
  }

  public alternarBtn(){
    this.nomeBtnFiltro = this.nomeBtnFiltro == "CPF" ? "Nome" : "CPF";
  }

  public get filtroLista(): string{
    return this._filtroLista;
  }

  public set filtroLista(value: string){
    this._filtroLista = value;
    this.clientesFiltrados = this._filtroLista ? this.filtrarClientes(this._filtroLista) : this.clientes;
  }

  public filtrarClientes(filtrarPor: string): Cliente[]{
    filtrarPor = filtrarPor.toLocaleLowerCase();
    if(this.nomeBtnFiltro == "Nome"){
      return this.clientes.filter(
        (cliente: any) => cliente.nome.toLocaleLowerCase().indexOf(filtrarPor) !== -1
      );
    }
    else{
      return this.clientes.filter(
        (cliente: any) => cliente.cpf.indexOf(filtrarPor) !== -1
      );
    }
  }

  private getClientesByVendedor(id: number): void{
    this.clienteService.getClienteByVendedor(id).subscribe(
      (clientes: Cliente[]) =>
      {
        this.clientesFiltrados = clientes;
      },
      (error: any) =>
      {
        console.log(error);
        this.toastr.error(error,"Erro!");
      }
    ).add(() => this.spinner.hide());
  }

  public getClientes(): void {
    this.clienteService.getClientes().subscribe(
      (clientes: Cliente[]) =>
      {
        this.clientes = clientes;
        this.clientesFiltrados = this.clientes;
      },
      (error: any) =>
      {
        console.log(error);
        this.toastr.error(error,"Erro!");
      }
    ).add(() => this.spinner.hide());
  }

  confirm(): void {
    this.modalRef?.hide();
    this.toastr.success('O cliente foi deletado com sucesso.', 'Deletado!');
  }

  decline(): void {
    this.modalRef?.hide();
  }

  openModal(template: TemplateRef<any>): void {
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }
}
