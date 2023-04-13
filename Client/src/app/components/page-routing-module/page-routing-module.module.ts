import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { MainPageComponent } from '../pages/main-page/main-page.component';
import { SplashComponent } from '../pages/splash/splash.component';
import { PersonalPageComponent } from '../pages/pesonal-page/personal-page.component';
import { AuthComponent } from '../layout-elements/auth/auth.component';
import { StocksComponent } from '../pages/stocks/stocks.component';
import { SecurityPageComponent } from '../pages/security-page/security-page.component';

const routes: Routes = [
  { path: '', component: MainPageComponent },
  { path: 'stocks', component: StocksComponent },
  { path: 'security-page', component: SecurityPageComponent },
  { path: 'splash', component: SplashComponent },
  { path: 'personal-page', component: PersonalPageComponent },
  { path: 'auth', component: AuthComponent },
]

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes)
  ],
  exports: [ RouterModule ]
})
export class PageRoutingModuleModule { }
