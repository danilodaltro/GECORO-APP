import { VendedoresComponent } from './components/vendedores/vendedores.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClientesComponent } from './components/clientes/clientes.component';
import { ContratosComponent } from './components/contratos/contratos.component';
import { ParcelasComponent } from './components/parcelas/parcelas.component';

const routes: Routes = [
  {path: 'vendedores', component: VendedoresComponent},
  {path: 'clientes', component: ClientesComponent},
  {path: 'contratos', component: ContratosComponent},
  {path: 'parcelas', component: ParcelasComponent},
  {path: '', redirectTo: 'contratos', pathMatch: 'full' },
  {path: '**', redirectTo: 'contratos', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
