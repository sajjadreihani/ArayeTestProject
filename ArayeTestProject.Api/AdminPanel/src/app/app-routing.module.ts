import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdminappComponent } from './admin-app/adminapp.component';

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'Admin' },
{ path: 'Admin', pathMatch: 'full', component: AdminappComponent },

{ path: '**', redirectTo: 'Admin' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
