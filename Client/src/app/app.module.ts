import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { DataTablesModule } from 'angular-datatables';

import { AppComponent } from './app.component';
import { AppConfig } from './app.config';
import { AuthComponent } from './components/layout-elements/auth/auth.component';
import { FooterComponent } from './components/layout-elements/footer/footer.component';
import { HeaderComponent } from './components/layout-elements/header/header.component';
import { PageRoutingModuleModule } from './components/page-routing-module/page-routing-module.module';
import { MainPageComponent } from './components/pages/main-page/main-page.component';
import { PersonalPageComponent } from './components/pages/pesonal-page/personal-page.component';
import { SecurityPageComponent } from './components/pages/security-page/security-page.component';
import { StocksComponent } from './components/pages/stocks/stocks.component';
import { SplashComponent } from './components/pages/splash/splash.component';
import { SecurityChatComponent } from './components/shared/security-chart/security-chart.component';
import { BaseService } from './services/base.service';
import { StockService } from './services/stock.service';
import { AccountsService } from './services/accounts.service';
import { AuthService } from './services/auth.service';
import { AlertComponent } from './components/shared/alert/alert.component';
import { AuthGuard } from './components/guards/auth.guard';
import { AlertService } from './services/alert.service';
import { AppState } from './components/models/app-state.module';

@NgModule({
  declarations: [
    AppComponent,
    AlertComponent,
    MainPageComponent,
    FooterComponent,
    HeaderComponent,
    StocksComponent,
    SplashComponent,
    PersonalPageComponent,
    AuthComponent,
    SecurityChatComponent,
    SecurityPageComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    PageRoutingModuleModule,
    ReactiveFormsModule,
    DataTablesModule
  ],
  providers: [ 
    AppConfig,
    AuthGuard,
    AppState, 
    AlertService,
    BaseService, 
    AccountsService,
    AuthService, 
    StockService 
  ],
  bootstrap: [ AppComponent ]
})
export class AppModule { }
