import { Component } from '@angular/core';
import { FormsService } from '../forms.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-agenda',
  templateUrl: './agenda.component.html',
  styleUrls: ['./agenda.component.css']
})
export class AgendaComponent {
  constructor(private formsService: FormsService, private router: Router) { }

  ci!: string;
  fechaAgenda!: Date;

  submitForm() {
    const datosFormulario = {
      ci: this.ci,
      fechaAgenda: this.fechaAgenda,
    }
    
    this.formsService.agendaForm(datosFormulario).subscribe({
      next: (response) => {
        console.log('Registro exitoso:', response);
        // Aquí puedes manejar la respuesta del backend, como redirigir a otra página, mostrar un mensaje, etc.
      },
      error: (error) => {
        console.error('Error en el registro:', error);
        // Manejar el error, como mostrar un mensaje de error al usuario
      }
    });
  };
}
