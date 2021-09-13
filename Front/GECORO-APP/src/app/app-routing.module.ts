import { ContratosDetalhesComponent } from './components/contratos/contratos-detalhes/contratos-detalhes.component';
import { VendedoresComponent } from './components/vendedores/vendedores.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClientesComponent } from './components/clientes/clientes.component';
import { ContratosComponent } from './components/contratos/contratos.component';
import { ParcelasComponent } from './components/parcelas/parcelas.component';
import { ContratosListaComponent } from './components/contratos/contratos-lista/contratos-lista.component';
import { ClientesDetalhesComponent } from './components/clientes/clientes-detalhes/clientes-detalhes.component';
import { ClientesListaComponent } from './components/clientes/clientes-lista/clientes-lista.component';
import { VendedoresDetalhesComponent } from './components/vendedores/vendedores-detalhes/vendedores-detalhes.component';
import { VendedoresListaComponent } from './components/vendedores/vendedores-lista/vendedores-lista.component';
import { ParcelasListaComponent } from './components/parcelas/parcelas-lista/parcelas-lista.component';

const routes: Routes = [
  {path: 'vendedores', redirectTo: 'vendedores/lista'},
  {path: 'vendedores', component: VendedoresComponent,
   children:[
    {path: 'detalhes', component: VendedoresDetalhesComponent},
    {path: 'lista', component: VendedoresListaComponent}
   ]
  },
  {path: 'clientes', redirectTo: 'clientes/lista'},
  {path: 'clientes', component: ClientesComponent,
   children:[
     {path: 'vendedor/:id', component: ClientesListaComponent},
     {path: 'detalhes', component: ClientesDetalhesComponent},
     {path: 'lista', component: ClientesListaComponent}
   ]
  },
  {path: 'contratos', redirectTo: 'contratos/lista'},
  {path: 'contratos', component: ContratosComponent,
   children: [
     {path: 'cliente/:id', component: ContratosListaComponent},
     {path: 'detalhes', component: ContratosDetalhesComponent},
     {path: 'lista', component: ContratosListaComponent}
   ]
  },
  {path: 'parcelas', component: ParcelasComponent,
   children: [{path: 'contrato/:id', component: ParcelasListaComponent}]
  },
  {path: '', redirectTo: 'contratos', pathMatch: 'full' },
  {path: '**', redirectTo: 'contratos', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
