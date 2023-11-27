import { Component } from '@angular/core';
import { LoginService } from '../login.service';
import { Router } from '@angular/router';
import { MessageService } from '../message.service';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.css']
})
export class LogoutComponent {
  constructor(private loginService: LoginService, private router: Router, private messageService: MessageService) { }

  logout(): void {
    this.loginService.removeSession();
    this.messageService.showMessage("¡Has salido de la sesión!")
    this.router.navigate(['/login']);
  }
}
