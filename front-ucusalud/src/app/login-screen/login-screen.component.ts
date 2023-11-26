import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../login.service';
import { catchError, throwError } from 'rxjs';

@Component({
  selector: 'app-login-screen',
  templateUrl: './login-screen.component.html',
  styleUrls: ['./login-screen.component.css']
})
export class LoginScreenComponent {
  showRegister = false;
  showLogin = false;
  username: string = '';
  password: string = '';

  constructor(private loginService: LoginService, private router: Router) { }

  showRegisterForm() {
    this.username = '';
    this.password = '';
    this.showRegister = true;
    this.showLogin = false;
  }

  showLoginForm() {
    this.username = '';
    this.password = '';
    this.showRegister = false;
    this.showLogin = true;
  }

  Register() {
    console.log('Register:', this.username, this.password);
    if (this.username && this.password) {
      this.loginService.register(this.username, this.password)
        .pipe(
          catchError((error) => {
            return throwError(() => error);
          })
        )
        .subscribe({
          next: (res: any) => {
            console.log("Usuario registrado!");
            this.router.navigateByUrl('/');
          },
          error: (err: any) => {
            console.log(err);
          },
        });
    }
  }

  Login() {
    console.log('LoginING:', this.username, 'Name:', this.password);
    if (this.username && this.password) {
      this.loginService.login(this.username, this.password)
        .pipe(
          catchError((error) => {
            return throwError(() => error);
          })
        )
        .subscribe({
          next: (res: any) => {
            console.log("logged in");
            this.router.navigateByUrl('/registered-form');
          },
          error: (err: any) => {
            console.log(err);
          },
        });
    }
  }
}