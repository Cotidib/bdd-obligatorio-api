import { Component } from '@angular/core';
import { FormsService } from '../forms.service';

@Component({
  selector: 'app-signup-form',
  templateUrl: './signup-form.component.html',
  styleUrls: ['./signup-form.component.css']
})
export class SignupFormComponent {

  constructor(private formsService: FormsService) { }

  ci!: string;
  nombre!: string;
  apellido!: string;
  fechaNacimiento!: Date;
  tieneCarneSalud: boolean = false;
  fechaEmision!: Date;
  fechaVencimiento!: Date;
  comprobante!: File;
  domicilio!: string;
  email!: string;
  telefono!: number;

  onFileChange(event: any) {
    this.comprobante = event.target.files[0];
  }

  submitForm() {
    // Aquí puedes manejar la lógica para enviar los datos a tu backend o realizar alguna acción con ellos.
    const datosFormulario = {
      ci: this.ci,
      nombre: this.nombre,
      apellido: this.apellido,
      fechaNacimiento: this.fechaNacimiento,
      tieneCarneSalud: this.tieneCarneSalud,
      fechaVencimiento: this.fechaVencimiento,
      fechaEmision: this.fechaEmision,
      comprobante: this.comprobante,
      domicilio: this.domicilio,
      email: this.email,
      telefono: this.telefono
    }
    this.formsService.signupForm(datosFormulario).subscribe({
      next: (response) => {
        console.log('Registro exitoso:', response);
        // Aquí puedes manejar la respuesta del backend, como redirigir a otra página, mostrar un mensaje, etc.
      },
      error: (error) => {
        console.error('Error en el registro:', error);
        // Manejar el error, como mostrar un mensaje de error al usuario
      }
    });
  }
}