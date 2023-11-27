import { Component } from '@angular/core';
import { FormsService } from '../forms.service';
import { Router } from '@angular/router';
import { MessageService } from '../message.service';

@Component({
  selector: 'app-registered-form',
  templateUrl: './registered-form.component.html',
  styleUrls: ['./registered-form.component.css']
})

export class RegisteredFormComponent {

  constructor(private formsService: FormsService, private router: Router, private messageService: MessageService) { }

  ci!: string;
  nombre!: string;
  apellido!: string;
  fechaNacimiento!: Date;
  tieneCarneSalud: boolean = false;
  fechaEmision!: Date;
  fechaVencimiento!: Date;
  comprobante!: File;

  onFileChange(event: any) {
    this.comprobante = event.target.files[0];
  };

  submitForm() {
    const datosFormulario = {
      ci: this.ci,
      nombre: this.nombre,
      apellido: this.apellido,
      fechaNacimiento: this.fechaNacimiento,
      tieneCarneSalud: this.tieneCarneSalud,
      fechaVencimiento: this.fechaVencimiento,
      fechaEmision: this.fechaEmision,
      comprobante: this.comprobante
    }
    this.formsService.registeredForm(datosFormulario).subscribe({
      next: (response) => {
        if (response && response.redirectUrl){
          console.log(response.redirectUrl);
          this.messageService.showMessage('No te encuentras en nuestros datos. Por favor, llena el formulario de alta.');
          this.router.navigateByUrl(response.redirectUrl);
        }
        else{
          this.messageService.showMessage('Registro exitoso!');
          console.log('Registro exitoso:', response);
        }
      },
      error: (error) => {
        this.messageService.showMessage('Error al enviar el formulario!');
        console.error('Error en el registro:', error);
      }
    });
  };
}