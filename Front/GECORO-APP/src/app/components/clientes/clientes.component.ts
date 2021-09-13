import { ClienteService } from './../../services/cliente.service';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Cliente } from 'src/app/models/Cliente';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-clientes',
  templateUrl: './clientes.component.html',
  styleUrls: ['./clientes.component.css']
})
export class ClientesComponent implements OnInit {
  modalRef?: BsModalRef;
  public clientes: Cliente[] = [];
  public clientesFiltrados: Cliente[] = [];
  constructor(
    private clienteService: ClienteService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit() {
    this.spinner.show();
    this.getClientes();
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
      },
      () => this.spinner.hide()
    )
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
