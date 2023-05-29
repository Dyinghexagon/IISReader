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
import { StockPageComponent } from './components/pages/stock-page/stock-page.component';
import { StocksComponent } from './components/pages/stocks/stocks.component';
import { SplashComponent } from './components/pages/splash/splash.component';
import { StockChatComponent } from './components/shared/stock-chart/stock-chart.component';
import { BaseService } from './services/base.service';
import { StockService } from './services/stock.service';
import { AccountsService } from './services/accounts.service';
import { AuthService } from './services/auth.service';
import { AlertModalComponent } from './components/shared/alert/alert.component';
import { AuthGuard } from './components/guards/auth.guard';
import { AlertService } from './services/alert.service';
import { AppState } from './components/models/app-state.module';
import { MdbModalService } from 'mdb-angular-ui-kit/modal';
import { AddNewStockListModalComponent } from './components/shared/modal/add-new-stock-list/add-new-stock-list-modal.component';
import { MdbFormsModule } from 'mdb-angular-ui-kit/forms';
import { ModalState } from './components/models/modal-state.module';
import { EditStockListModalComponent } from './components/shared/modal/edit-stock-list/edit-stock-list-modal.component';
import { NotificatedService } from './services/notification.service';
import { NotificationList as NotificationListComponent } from './components/layout-elements/notification-list/notification-list.component';
import { NotificationItem as NotificationItemComponent } from './components/layout-elements/notification-list/notification-item/notification-item.component';
import { NgxPaginationModule } from 'ngx-pagination';
import { SelectCalculationType as SelectCalculationTypeComponent } from './components/shared/select-calculation-type/select-calculation-type.component';
import { ManageStockComponent } from './components/shared/manage-stock/manage-stock.component';
import { ManageStocksModalComponent } from './components/shared/modal/manage-stocks/manage-stocks-modal.component';

@NgModule({
  declarations: [
    AppComponent,
    AlertModalComponent,
    MainPageComponent,
    FooterComponent,
    HeaderComponent,
    StocksComponent,
    SplashComponent,
    PersonalPageComponent,
    AuthComponent,
    StockChatComponent,
    StockPageComponent,
    AddNewStockListModalComponent,
    EditStockListModalComponent,
    NotificationListComponent,
    NotificationItemComponent,
    SelectCalculationTypeComponent,
    ManageStockComponent,
    ManageStocksModalComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    PageRoutingModuleModule,
    ReactiveFormsModule,
    DataTablesModule,
    MdbFormsModule,
    NgxPaginationModule
  ],
  providers: [ 
    AppConfig,
    AuthGuard,
    AppState,
    ModalState,
    AlertService,
    BaseService, 
    AccountsService,
    AuthService, 
    StockService,
    MdbModalService,
    NotificatedService
  ],
  bootstrap: [ AppComponent ]
})
export class AppModule { }
