import { Component } from '@angular/core';
import { FormsService } from '../forms.service';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MessageService } from '../message.service';

@Component({
  selector: 'app-signup-form',
  templateUrl: './signup-form.component.html',
  styleUrls: ['./signup-form.component.css']
})
export class SignupFormComponent {

  currentDate: Date;

  formulario: FormGroup;

  constructor(private formsService: FormsService, private fb: FormBuilder, private router: Router, private messageService: MessageService) {
    this.currentDate = new Date();
    this.formulario = this.fb.group({
      //ci: ['', Validators.required],
      //nombre: ['', [Validators.minLength(3), Validators.maxLength(50)]],
      // apellido: ['', [Validators.minLength(3), Validators.maxLength(50)]],
      // domicilio: ['', [Validators.minLength(3), Validators.maxLength(50)]],
      // email: ['', [Validators.required, Validators.email]],
      // telefono: ['', Validators.required],
      // fechaNacimiento: ['', Validators.required]
    });
  }

  errorMessage: string | null = null;

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
    if (this.comprobante) {
      const allowedExtensions = ['.jpg', '.jpeg', '.pdf'];
      const fileExtension = this.comprobante.name.toLowerCase().slice((Math.max(0, this.comprobante.name.lastIndexOf(".")) || Infinity) + 1);

      if (allowedExtensions.indexOf(`.${fileExtension}`) === -1) {
        console.log('Tipo de archivo no permitido.');
        return;
      }
    }
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

    if(this.tieneCarneSalud == false || this.fechaVencimiento <= this.currentDate){

    }
    this.formsService.signupForm(datosFormulario).subscribe({
      next: (response) => {
        if (response && response.redirectUrl){
          console.log(response.redirectUrl);
          this.messageService.showMessage(response.mensaje);
          this.router.navigateByUrl(response.redirectUrl);
        }
        else{
          this.messageService.showMessage(response.mensaje);
          console.log('Registro exitoso:', response);
        }
      },
      error: (error) => {
        console.error('Error en el registro:', error);
        this.errorMessage = 'Se produjo un error durante el registro. Por favor, inténtalo de nuevo más tarde.';
      }
    });
  }
}