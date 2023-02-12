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

  public async ngOnInit(): Promise<void> {
    const account = await this.appState.getAccount();
    if (account) {
      this.isLogin = true;
    }

    this.appState.authState.login$.subscribe(() => {
      this.isLogin = true;
    });

    this.appState.authState.logout$.subscribe(() => {
      this.isLogin = false;
    })
  }

}
