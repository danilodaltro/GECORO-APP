import { CpfPipePipe } from './util/cpfPipe.pipe';
import { CUSTOM_ELEMENTS_SCHEMA, DEFAULT_CURRENCY_CODE, LOCALE_ID, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http'
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { CollapseModule } from 'ngx-bootstrap/collapse';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { ModalModule } from 'ngx-bootstrap/modal';
import { ToastrModule } from 'ngx-toastr';
import { NgxSpinnerModule } from 'ngx-spinner';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { VendedoresComponent } from './components/vendedores/vendedores.component';
import { ClientesComponent } from './components/clientes/clientes.component';
import { ContratosComponent } from './components/contratos/contratos.component';
import { ParcelasComponent } from './components/parcelas/parcelas.component';
import { NavComponent } from './shared/nav/nav.component';
import { TituloComponent } from './shared/titulo/titulo.component';

import { ContratoService } from './services/contrato.service';
import { ClienteService } from './services/cliente.service';
import { VendedorService } from './services/vendedor.service';
import { ParcelaService } from './services/parcela.service';
import { registerLocaleData } from '@angular/common';
import ptBr from '@angular/common/locales/pt';
import { ContratosDetalhesComponent } from './components/contratos/contratos-detalhes/contratos-detalhes.component';
import { ContratosListaComponent } from './components/contratos/contratos-lista/contratos-lista.component';
import { ClientesDetalhesComponent } from './components/clientes/clientes-detalhes/clientes-detalhes.component';
import { ClientesListaComponent } from './components/clientes/clientes-lista/clientes-lista.component';
import { VendedoresDetalhesComponent } from './components/vendedores/vendedores-detalhes/vendedores-detalhes.component';
import { VendedoresListaComponent } from './components/vendedores/vendedores-lista/vendedores-lista.component';
import { ParcelasListaComponent } from './components/parcelas/parcelas-lista/parcelas-lista.component';
import { ParcelasDetalhesComponent } from './components/parcelas/parcelas-detalhes/parcelas-detalhes.component';

registerLocaleData(ptBr);

@NgModule({
  declarations: [
    AppComponent,
    VendedoresComponent,
    VendedoresDetalhesComponent,
    VendedoresListaComponent,
    ClientesComponent,
    ClientesDetalhesComponent,
    ClientesListaComponent,
    ContratosComponent,
    ContratosDetalhesComponent,
    ContratosListaComponent,
    ParcelasComponent,
    ParcelasListaComponent,
    ParcelasDetalhesComponent,
    NavComponent,
    TituloComponent,
    CpfPipePipe
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    CollapseModule.forRoot(),
    BsDropdownModule.forRoot(),
    TooltipModule.forRoot(),
    ModalModule.forRoot(),
    NgxSpinnerModule,
    ToastrModule.forRoot({
      timeOut: 5000,
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
      progressBar: true
    }),
    ReactiveFormsModule
  ],
  providers: [
    ContratoService,
    ClienteService,
    VendedorService,
    ParcelaService,
    { provide: LOCALE_ID, useValue: 'pt' }
  ],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class AppModule { }
