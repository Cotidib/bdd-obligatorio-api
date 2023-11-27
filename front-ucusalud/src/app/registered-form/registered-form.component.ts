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

  ci: string = '';
  nombre: string = '';
  apellido: string = '';
  fechaNacimiento: Date | null = null;
  tieneCarneSalud: boolean = false;
  fechaEmision: Date | null = null;
  fechaVencimiento: Date | null = null;
  comprobante: File | null = null;

  onFileChange(event: any) {
    this.comprobante = event.target.files[0];
  };

  submitForm() {
    if (this.isValid()) {
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
          if (response && response.redirectUrl) {
            console.log(response.redirectUrl);
            this.messageService.showMessage(response.mensaje);
            this.router.navigateByUrl(response.redirectUrl);
          }
          else {
            this.messageService.showMessage(response.mensaje);
            console.log('Registro exitoso:', response);
          }
        },
        error: (error) => {
          this.messageService.showMessage('Error al enviar el formulario!');
          console.error('Error en el registro:', error);
        }
      });
    } else {
      this.messageService.showMessage('Por favor, complete el formulario correctamente.');
    }
  };
  isValid(): boolean {
    const isCiValid = /^\d+$/.test(this.ci); //cadena de números
    const isNombreValid = /^[a-zA-ZáéíóúüÁÉÍÓÚÜ\s']{1,50}$/.test(this.nombre); // con o sin tildes y una longitud máxima de 50
    const isApellidoValid = /^[a-zA-ZáéíóúüÁÉÍÓÚÜ\s']{1,50}$/.test(this.apellido);

    return isCiValid && isNombreValid && isApellidoValid && !!this.fechaNacimiento;
  }
}