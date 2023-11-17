import { Component } from '@angular/core';

@Component({
  selector: 'app-registered-form',
  templateUrl: './registered-form.component.html',
  styleUrls: ['./registered-form.component.css']
})

export class RegisteredFormComponent {

  ci!: string;
  nombre!: string;
  fechaNacimiento!: Date;
  tieneCarneSalud: boolean = false;
  fechaVencimiento!: Date;
  comprobante!: File;

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
      comprobante: this.comprobante
    });
  }
}
