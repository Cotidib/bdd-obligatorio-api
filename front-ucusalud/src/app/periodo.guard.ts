import { ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { LoginService } from './login.service';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { FormsService } from './forms.service';

@Injectable({
  providedIn: 'root',
})
export class PeriodoGuard {
  constructor(private loginService: LoginService, private formsService: FormsService, private router: Router) { }

  async canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Promise<boolean> {
    try {
      const value = await this.loginService.isAuthenticated().toPromise();
      const periodo = await this.formsService.getPeriodo().toPromise();

      if (value === true) {
        if(!this.isWithinPeriodo(periodo)){
          this.router.navigateByUrl('/periodofinalizado');
        }
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

  private isWithinPeriodo(periodo: any): boolean {
    const currentDate = new Date();
    const inicioPeriodo = new Date(periodo.fch_Inicio);
    const finPeriodo = new Date(periodo.fch_Fin);

    console.log(inicioPeriodo, " ", finPeriodo, ",",currentDate);

    return (currentDate >= inicioPeriodo && currentDate <= finPeriodo);
  }
}