import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RegisteredFormComponent } from './registered-form/registered-form.component';
import { FormsModule } from '@angular/forms';
import { DashboardComponent } from './dashboard/dashboard.component';
import { SignupFormComponent } from './signup-form/signup-form.component';
import { LoginScreenComponent } from './login-screen/login-screen.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { AuthinterceptorService } from './authinterceptor.service';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AgendaComponent } from './agenda/agenda.component';
import { PeriodoFinalizadoComponent } from './periodo-finalizado/periodo-finalizado.component';
import { CambiarPeriodoComponent } from './cambiar-periodo/cambiar-periodo.component';
import { MessageBarComponent } from './message-bar/message-bar.component';
import { LogoutComponent } from './logout/logout.component';

@NgModule({
  declarations: [
    AppComponent,
    RegisteredFormComponent,
    DashboardComponent,
    SignupFormComponent,
    LoginScreenComponent,
    PageNotFoundComponent,
    AgendaComponent,
    PeriodoFinalizadoComponent,
    CambiarPeriodoComponent,
    MessageBarComponent,
    LogoutComponent,
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    FormsModule,
    AppRoutingModule
  ],
  providers: [AuthinterceptorService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthinterceptorService,
      multi: true
    }],
  bootstrap: [AppComponent]
})
export class AppModule { }
