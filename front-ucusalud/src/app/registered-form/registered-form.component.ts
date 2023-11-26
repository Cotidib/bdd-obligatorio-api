import { Component } from '@angular/core';
import { FormsService } from '../forms.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registered-form',
  templateUrl: './registered-form.component.html',
  styleUrls: ['./registered-form.component.css']
})

export class RegisteredFormComponent {

  constructor(private formsService: FormsService, private router: Router) { }

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
          this.router.navigateByUrl(response.redirectUrl);
        }
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