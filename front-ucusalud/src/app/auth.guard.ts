import { ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { LoginService } from './login.service';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { FormsService } from './forms.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard {
  constructor(private loginService: LoginService, private formsService: FormsService, private router: Router) { }

  async canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Promise<boolean> {
    try {
      const value = await this.loginService.isAuthenticated().toPromise();

      if (value === true) {
        return true;
      } else {
        this.router.navigateByUrl('/login');
        return false;
      }
    } catch (error) {
      this.router.navigateByUrl('/login');
      console.error('ERROR: durante la autenticación:', error);
      return false;
    }
  }
}