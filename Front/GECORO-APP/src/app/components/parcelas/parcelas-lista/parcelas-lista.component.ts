import { Component, OnInit, TemplateRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Parcela } from 'src/app/models/Parcela';
import { ParcelaService } from 'src/app/services/parcela.service';

@Component({
  selector: 'app-parcelas-lista',
  templateUrl: './parcelas-lista.component.html',
  styleUrls: ['./parcelas-lista.component.css']
})
export class ParcelasListaComponent implements OnInit {

  modalRef?: BsModalRef;
  public parcelas: Parcela[] = [];
  constructor(
    private parcelaService: ParcelaService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private activeRoute: ActivatedRoute
  ) { }

  ngOnInit() {
    this.spinner.show();
    const idContrato = this.activeRoute.snapshot.paramMap.get("id");
    console.log(idContrato);
    if(idContrato != null){
      this.getParcelas(+idContrato);
    }
  }

  public getParcelas(idContrato: number): void {
    this.parcelaService.getParcelasByContrato(idContrato).subscribe(
      (parcelas: Parcela[]) =>
      {
        this.parcelas = parcelas;
      },
      (error: any) =>
      {
        console.log(error);
        this.toastr.error(error,"Erro!");
      },
      () => this.spinner.hide()
    )
  }

  confirm(): void {
    this.modalRef?.hide();
    this.toastr.success('A parcela foi deletada com sucesso.', 'Deletada!');
  }

  decline(): void {
    this.modalRef?.hide();
  }

  openModal(template: TemplateRef<any>): void {
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

}
