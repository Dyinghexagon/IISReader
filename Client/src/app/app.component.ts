import { Component, OnInit } from '@angular/core';
import { NotificatedService } from './services/notification.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Client';

  constructor(
    public notificationService: NotificatedService, 
  ) {}

  public ngOnInit(): void {
    this.notificationService.startConnection();
    this.notificationService.addTransferStockDataListener();
  }
}