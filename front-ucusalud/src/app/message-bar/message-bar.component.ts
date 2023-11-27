import { Component, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { MessageService } from '../message.service';

@Component({
  selector: 'app-message-bar',
  templateUrl: './message-bar.component.html',
  styleUrls: ['./message-bar.component.css']
})
export class MessageBarComponent implements OnDestroy {
  message: string | null = null;
  private subscription: Subscription;

  constructor(private messageService: MessageService) {
    this.subscription = this.messageService.message$.subscribe((message) => {
      this.message = message;
      setTimeout(() => {
        this.message = null;
      }, 1800
      );
    });
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}