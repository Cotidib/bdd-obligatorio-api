import { Component } from '@angular/core';

@Component({
  selector: 'app-signup-form',
  templateUrl: './signup-form.component.html',
  styleUrls: ['./signup-form.component.css']
})
export class SignupFormComponent {
  ci!: string;
  nombre!: string;
  fechaNacimiento!: Date;
  tieneCarneSalud: boolean = false;
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
    console.log('Datos del formulario:', {
      ci: this.ci,
      nombre: this.nombre,
      fechaNacimiento: this.fechaNacimiento,
      tieneCarneSalud: this.tieneCarneSalud,
      fechaVencimiento: this.fechaVencimiento,
      comprobante: this.comprobante,
      domicilio: this.domicilio,
      email: this.email,
      telefono: this.telefono
    });
  }
}