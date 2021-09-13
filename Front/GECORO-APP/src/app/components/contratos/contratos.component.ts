import { ContratoService } from './../../services/contrato.service';
import { Component, Input, OnInit, TemplateRef } from '@angular/core';
import { Contrato } from 'src/app/models/Contrato';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-contratos',
  templateUrl: './contratos.component.html',
  styleUrls: ['./contratos.component.css']
})
export class ContratosComponent implements OnInit {
  //@Input() idCliente: number = 0;
  modalRef?: BsModalRef;
  public contratos: Contrato[] = [];
  public contratosFiltrados: Contrato[] = [];
  constructor(
    private contratoService: ContratoService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit() {
    this.spinner.show();
    this.getContratos();
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
      },
      () => this.spinner.hide()
    )
  }

  // public getContratosByCliente(): void{
  //   this.contratoService.getContratosByCliente(this.idCliente).subscribe(
  //     (contratos: Contrato[]) =>{
  //       this.contratos = contratos;
  //       this.contratosFiltrados = this.contratos;
  //     },
  //     (error: any) =>
  //     {
  //       console.log(error);
  //       this.toastr.error(error,"Erro!");
  //     },
  //     () => this.spinner.hide()
  //   )
  // }

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
