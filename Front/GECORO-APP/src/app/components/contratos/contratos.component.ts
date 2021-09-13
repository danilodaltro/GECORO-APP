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
  ngOnInit() {

  }
}
