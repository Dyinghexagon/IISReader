import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { AppConfig } from './app.config';
import { AuthComponent } from './components/layout-elements/auth/auth.component';
import { FooterComponent } from './components/layout-elements/footer/footer.component';
import { HeaderComponent } from './components/layout-elements/header/header.component';
import { PageRoutingModuleModule } from './components/page-routing-module/page-routing-module.module';
import { MainPageComponent } from './components/pages/main-page/main-page.component';
import { PersonalPageComponent } from './components/pages/pesonal-page/personal-page.component';
import { SecuritysListComponent } from './components/pages/securitys-list/securitys-list.component';
import { SplashComponent } from './components/pages/splash/splash.component';
import { BaseService } from './services/base.service';
import { SecurityService } from './services/securitys.service';
import { UserService } from './services/user.services';

@NgModule({
  declarations: [
    AppComponent,
    MainPageComponent,
    FooterComponent,
    HeaderComponent,
    SecuritysListComponent,
    SplashComponent,
    PersonalPageComponent,
    AuthComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
    FormsModule,
    PageRoutingModuleModule,
    ReactiveFormsModule
  ],
  providers: [ AppConfig, BaseService, UserService, SecurityService ],
  bootstrap: [AppComponent]
})
export class AppModule { }
