import { Component, OnInit, TemplateRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Contrato } from 'src/app/models/Contrato';
import { ContratoService } from 'src/app/services/contrato.service';

@Component({
  selector: 'app-contratos-lista',
  templateUrl: './contratos-lista.component.html',
  styleUrls: ['./contratos-lista.component.css']
})
export class ContratosListaComponent implements OnInit {

  modalRef?: BsModalRef;
  public contratos: Contrato[] = [];
  public contratosFiltrados: Contrato[] = [];
  private _filtroLista: string = "";
  constructor(
    private contratoService: ContratoService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private activeRoute: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit() {
    this.spinner.show();
    if(this.router.url.includes("cliente")){
      const clienteId = this.activeRoute.snapshot.paramMap.get("id");
      if(clienteId != null){
        this.getContratosByCliente(+clienteId);
      }
    }
    else this.getContratos();
    this.spinner.hide();
  }

  public get filtroLista(): string{
    return this._filtroLista;
  }

  public set filtroLista(value: string){
    this._filtroLista = value;
    this.contratosFiltrados = this._filtroLista ? this.filtrarContratos(this._filtroLista) : this.contratos;
  }

  public filtrarContratos(filtrarPor: string): Contrato[]{
    filtrarPor = filtrarPor.toLocaleLowerCase();
      return this.contratos.filter(
        (contrato: any) => contrato.nuContrato.indexOf(filtrarPor) !== -1
      );
  }

  private getContratosByCliente(id: number): void{
    this.contratoService.getContratosByCliente(id).subscribe(
      (contratos: Contrato[]) =>
      {
        this.contratosFiltrados = contratos;
      },
      (error: any) =>
      {
        console.log(error);
        this.toastr.error(error,"Erro!");
      }
    ).add(() => this.spinner.hide());
  }

  public getContratos(): void {
    this.contratoService.getContratos().subscribe(
      (contratos: Contrato[]) =>
      {
        this.contratos = contratos;
        this.contratosFiltrados = this.contratos;
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
