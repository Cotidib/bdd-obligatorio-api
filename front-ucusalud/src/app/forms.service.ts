import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, map, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FormsService {

  private apiUrl = 'http://localhost:5000'; // Reemplaza con la URL de tu backend

  constructor(private http: HttpClient) { }

  signupForm(datos: any): Observable<any> {
    const formData = new FormData();

    formData.append('ci', datos.ci);
    formData.append('nombre', datos.nombre);
    formData.append('apellido', datos.apellido);
    formData.append('fechaNacimiento', datos.fechaNacimiento.toString());
    formData.append('tieneCarneSalud', datos.tieneCarneSalud.toString());
    if (datos.tieneCarneSalud) {
      formData.append('fechaVencimiento', datos.fechaVencimiento.toString());
      formData.append('fechaEmision', datos.fechaEmision.toString());
      formData.append('comprobante', datos.comprobante);
    }
    formData.append('domicilio', datos.domicilio);
    formData.append('email', datos.email);
    formData.append('telefono', datos.telefono);

    const headers = new HttpHeaders();
    headers.append('Content-Type', 'multipart/form-data');

    return this.http.post(`${this.apiUrl}/archivos/uploadSignup`, formData, { headers });
  }
  registeredForm(datos: any): Observable<any> {
    const formData = new FormData();

    formData.append('ci', datos.ci);
    formData.append('nombre', datos.nombre);
    formData.append('apellido', datos.apellido);
    formData.append('fechaNacimiento', datos.fechaNacimiento.toString());
    if (datos.tieneCarneSalud) {
      formData.append('tieneCarneSalud', datos.tieneCarneSalud.toString());
      formData.append('fechaVencimiento', datos.fechaVencimiento.toString());
      formData.append('fechaEmision', datos.fechaEmision.toString());
      formData.append('comprobante', datos.comprobante);
    }
    const headers = new HttpHeaders();
    headers.append('Content-Type', 'multipart/form-data');

    return this.http.post(`${this.apiUrl}/archivos/uploadFile`, formData, { headers });
  }

  agendaForm(datos: any): Observable<any> {
    const formData = new FormData();

    formData.append('ci', datos.ci);
    formData.append('Fch_Agenda', datos.fechaAgenda.toString());

    const headers = new HttpHeaders();
    headers.append('Content-Type', 'multipart/form-data');

    return this.http.post(`${this.apiUrl}/agenda`, formData, { headers });
  }

  getPeriodo(): Observable<any> {
    return this.http.get(`${this.apiUrl}/periodoactual`).pipe(
      map((response: any) => response), 
      catchError(error => {
        console.error('Error al obtener el periodo:', error);
        return of(null);
      }));
  }

  setPeriodo(datos: any): Observable<any> {
    const formData = new FormData();

    formData.append('Anio', datos.Anio);
    formData.append('Semestre', datos.Semestre.toString());
    formData.append('Fch_Inicio', datos.Fch_Inicio.toString());
    formData.append('Fch_Fin', datos.Fch_Fin.toString());

    const headers = new HttpHeaders();
    headers.append('Content-Type', 'multipart/form-data');

    return this.http.put(`${this.apiUrl}/periodoactual`, formData, { headers });
  }
}
