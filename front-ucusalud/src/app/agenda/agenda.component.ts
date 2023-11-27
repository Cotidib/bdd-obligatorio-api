import { Component } from '@angular/core';
import { FormsService } from '../forms.service';
import { Router } from '@angular/router';
import { MessageService } from '../message.service';

@Component({
  selector: 'app-agenda',
  templateUrl: './agenda.component.html',
  styleUrls: ['./agenda.component.css']
})
export class AgendaComponent {
  constructor(private formsService: FormsService, private router: Router, private messageService: MessageService) { }

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
        this.messageService.showMessage('Agenda exitosa.');
        this.router.navigateByUrl('/');
      },
      error: (error) => {
        console.error('Error en el registro:', error);
        this.messageService.showMessage('Error en la agenda. Intente más tarde.');
        // Manejar el error, como mostrar un mensaje de error al usuario
      }
    });
  };
}
