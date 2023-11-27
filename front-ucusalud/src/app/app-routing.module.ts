import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { RegisteredFormComponent } from './registered-form/registered-form.component';
import { SignupFormComponent } from './signup-form/signup-form.component';
import { LoginScreenComponent } from './login-screen/login-screen.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { HttpClientModule } from '@angular/common/http';
import { AuthGuard } from './auth.guard';
import { AgendaComponent } from './agenda/agenda.component';
import { PeriodoFinalizadoComponent } from './periodo-finalizado/periodo-finalizado.component';
import { PeriodoGuard } from './periodo.guard';
import { CambiarPeriodoComponent } from './cambiar-periodo/cambiar-periodo.component';

const routes: Routes = [
  { path: 'login', title: 'Autenticación', component: LoginScreenComponent },
  {
    path: '', redirectTo: '/dashboard', pathMatch: 'full'
  },
  {
    path: 'dashboard', title: 'Dashboard', component: DashboardComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'registered-form', title: 'Formulario', component: RegisteredFormComponent,
    canActivate: [PeriodoGuard],
  },
  {
    path: 'signup-form', title: 'Formulario Signup', component: SignupFormComponent,
    canActivate: [PeriodoGuard],
  },
  {
    path: 'agenda', title: 'Agendarse', component: AgendaComponent,
    canActivate: [PeriodoGuard],
  },
  {
    path: 'cambiar-periodo', title: 'Cambiar periodo', component: CambiarPeriodoComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'periodofinalizado', title: 'Finalizado :(', component: PeriodoFinalizadoComponent,
  },
  { path: '**', title: 'No encontrado :(', component: PageNotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes), HttpClientModule],
  exports: [RouterModule]
})
export class AppRoutingModule { }
