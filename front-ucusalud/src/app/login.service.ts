import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { shareReplay, map, catchError } from 'rxjs/operators'
import { Observable } from 'rxjs';

interface AuthResult {
  token: string
}

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private http: HttpClient) {
  }

  login(username: string, password: string) {
    return this.http.post<AuthResult>('http://localhost:5000/login', { username, password }).pipe(map((authResult: AuthResult) => this.setSession(authResult))).pipe(shareReplay());
  }

  register(username: string, password: string){
    return this.http.post<AuthResult>('http://localhost:5000/register', { username, password }).pipe(map((authResult: AuthResult) => this.setSession(authResult))).pipe(shareReplay());
  }

  private setSession(authResult: AuthResult) {
    localStorage.setItem('id_token', authResult.token);
    console.log("Se ha guardado el token",authResult.token);
  }

  public removeSession(){
    localStorage.removeItem('id_token');
  }

  obtainAuthStatus() {
    return this.http.get<any>('http://localhost:5000/authmiddleware')
      .pipe(
        catchError((error) => {
          console.error('Error occurred:', error);
          throw error;
        })
      )
  }

  isAuthenticated(): Observable<boolean> {
    return this.obtainAuthStatus().pipe(
      map((response) => {
        const codigoEstado = response.status;
        console.log("CodigoEstado isAuthenticated:",response);
        if (codigoEstado === 200 && response.autenticado) {
          return true;
        } else {
          throw new Error('Fallo de auth');
        }
      }),
      catchError((error) => {
        console.error('Error:', error);
        throw error;
      })
    );
  }

  
}