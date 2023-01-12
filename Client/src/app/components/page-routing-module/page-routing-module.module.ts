import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { MainPageComponent } from '../pages/main-page/main-page.component';
import { SecurityComponent } from '../pages/security/security.component';
import { SplashComponent } from '../pages/splash/splash.component';
import { PersonalPageComponent } from '../pages/pesonal-page/personal-page.component';
import { AuthComponent } from '../layout-elements/auth/auth.component';


const routes: Routes = [
  { path: '', component: MainPageComponent },
  { path: 'security', component: SecurityComponent },
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
