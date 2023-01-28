import { Component, OnInit } from '@angular/core';
import { AppState } from '../../models/app-state.module';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  public isLogin: boolean = false;

  constructor(private readonly appState: AppState) { }

  public ngOnInit(): void {
    if (localStorage.getItem("currentAccount")) {
      this.isLogin = true;
    }
    this.appState.authState.login$.subscribe(() => {
      this.isLogin = true;
    });
  }

}
