import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FormsService {

  private apiUrl = 'http://localhost:5000/archivos'; // Reemplaza con la URL de tu backend

  constructor(private http: HttpClient) { }

  signupForm(datos: any): Observable<any> {
    const formData = new FormData();

    formData.append('ci', datos.ci);
    formData.append('nombre', datos.nombre);
    formData.append('apellido', datos.apellido);
    formData.append('fechaNacimiento', datos.fechaNacimiento.toString());
    formData.append('tieneCarneSalud', datos.tieneCarneSalud.toString());
    formData.append('fechaVencimiento', datos.fechaVencimiento.toString());
    formData.append('fechaEmision', datos.fechaEmision.toString());
    formData.append('domicilio', datos.domicilio);
    formData.append('email', datos.email);
    formData.append('telefono', datos.telefono);

    formData.append('comprobante', datos.comprobante);

    const headers = new HttpHeaders();
    headers.append('Content-Type', 'multipart/form-data');

    return this.http.post(`${this.apiUrl}/uploadSignup`, formData, { headers });
  }
  registeredForm(datos: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/uploadRegistered`, datos);
  }
}
