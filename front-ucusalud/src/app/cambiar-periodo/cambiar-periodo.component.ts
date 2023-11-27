import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormsService } from '../forms.service';
import { MessageService } from '../message.service';

@Component({
  selector: 'app-cambiar-periodo',
  templateUrl: './cambiar-periodo.component.html',
  styleUrls: ['./cambiar-periodo.component.css']
})
export class CambiarPeriodoComponent {

  mensajeRespuesta: string | null = null;

  constructor(private formsService: FormsService, private router: Router, private messageService: MessageService) { }

  anio!: number;
  semestre!: number;
  fchInicio!: Date;
  fchFin!: Date;

  cambiarPeriodo() {
    const datosFormulario = {
      Anio: this.anio,
      Semestre: this.semestre,
      Fch_Inicio: this.fchInicio,
      Fch_Fin: this.fchFin,
    }

    this.formsService.setPeriodo(datosFormulario).subscribe({
      next: (response) => {
        if (response && response.redirectUrl) {
          this.mensajeRespuesta = response.mensaje;
          console.log(response.redirectUrl);
        }
        this.messageService.showMessage('Periodo cambiado.');
        console.log('Registro exitoso:', response);
        console.log('Nuevo periodo:', this.anio, this.semestre, this.fchInicio, this.fchFin);
      },
      error: (error) => {
        console.error('Error en el registro:', error);
        this.messageService.showMessage('Error al cambiar de periodo.');

      }
    });;
  }
}
