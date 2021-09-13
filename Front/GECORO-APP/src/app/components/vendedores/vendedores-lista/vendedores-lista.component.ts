import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Vendedor } from 'src/app/models/Vendedor';
import { ContratoService } from 'src/app/services/contrato.service';
import { VendedorService } from 'src/app/services/vendedor.service';

@Component({
  selector: 'app-vendedores-lista',
  templateUrl: './vendedores-lista.component.html',
  styleUrls: ['./vendedores-lista.component.css']
})
export class VendedoresListaComponent implements OnInit {


  modalRef?: BsModalRef;
  public vendedores: Vendedor[] = [];
  public vendedoresFiltrados: Vendedor[] = [];
  private _filtroLista: string = "";
  public nomeBtnFiltro: string = "Código";
  constructor(
    private vendedorService: VendedorService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit() {
    this.spinner.show();
    this.getContratos();
  }

  public alternarBtn(){
    this.nomeBtnFiltro = this.nomeBtnFiltro == "Código" ? "Nome" : "Código";
  }

  public get filtroLista(): string{
    return this._filtroLista;
  }

  public set filtroLista(value: string){
    this._filtroLista = value;
    this.vendedoresFiltrados = this._filtroLista ? this.filtrarVendedores(this._filtroLista) : this.vendedores;
  }

  public filtrarVendedores(filtrarPor: string): Vendedor[]{
    filtrarPor = filtrarPor.toLocaleLowerCase();
    if(this.nomeBtnFiltro == "Nome"){
      return this.vendedores.filter(
        (vendedor: any) => vendedor.nome.toLocaleLowerCase().indexOf(filtrarPor) !== -1
      );
    }
    else{
      return this.vendedores.filter(
        (vendedor: any) => vendedor.codigo.indexOf(filtrarPor) !== -1
      );
    }
  }

  public getContratos(): void {
    this.vendedorService.getVendedores().subscribe(
      (vendedores: Vendedor[]) =>
      {
        this.vendedores = vendedores;
        this.vendedoresFiltrados = this.vendedores;
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
