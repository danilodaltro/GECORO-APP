import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Vendedor } from 'src/app/models/Vendedor';
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
  public vendedorDeletar = {} as Vendedor;
  constructor(
    private vendedorService: VendedorService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit() {
    this.spinner.show();
    this.getVendedores();
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

  public getVendedores(): void {
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
    this.spinner.show();

   this.vendedorService.delete(this.vendedorDeletar.id).subscribe(
     (result: any) =>{
          this.toastr.success('O vendedor foi deletado com sucesso.', 'Deletado!');
          this.getVendedores();
      },
     (error: any) => {
       console.error(error);
       this.toastr.error('Erro ao deletar vendedor!', 'Erro!');
     },

   ).add(() => this.spinner.hide());
  }

  decline(): void {
    this.modalRef?.hide();
  }

  openModal(template: TemplateRef<any>, vend: Vendedor): void {
    this.vendedorDeletar = vend;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

}
