import { Component, OnDestroy, OnInit  } from "@angular/core";
import { Router } from "@angular/router";
import { MdbModalRef, MdbModalService } from "mdb-angular-ui-kit/modal";
import { Subject, takeUntil } from "rxjs";
import { AccountsService } from "src/app/services/accounts.service";
import { AuthService } from "src/app/services/auth.service";
import { NotificatedService } from "src/app/services/notification.service";
import { AccountModel } from "../../models/account.model";
import { AppState } from "../../models/app-state.module";
import { ModalState } from "../../models/modal-state.module";
import { StockListModel } from "../../models/stock-list.model";
import { AddNewStockListComponent } from "../../shared/modal/add-new-stock-list/add-new-stock-list-modal.component";
import { EditStockListComponent } from "../../shared/modal/edit-stock-list/edit-stock-list.component";

@Component({
    selector: "personal-page",
    templateUrl: "./personal-page.component.html",
    styleUrls: [ "./personal-page.component.scss" ]
})

export class PersonalPageComponent implements OnInit, OnDestroy {

    public account!: AccountModel | null;
    public tab: "planning" | "notification" = "notification";
    public modalRef: MdbModalRef<AddNewStockListComponent> | null = null;
    public dtOptions: DataTables.Settings = {};
    public dtTrigger: Subject<any> = new Subject<StockListModel[]>();

    private readonly unsubscribe$ = new Subject<void>();

    constructor(
        private readonly router: Router,
        private readonly appState: AppState,
        private readonly modalState: ModalState,
        private readonly authService: AuthService,
        private readonly modalService: MdbModalService,
        private readonly accountService: AccountsService,
        private readonly notificatedService: NotificatedService
    ) {
    }

    public async ngOnInit(): Promise<void>  {
        this.account = await this.appState.getAccount();

        this.notificatedService.notificateSend$
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe(async () => {
                this.account = await this.appState.getAccount();
            });

        this.modalState.addNewStockList.createdStockList$
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe(async (stockList: StockListModel) => {
                this.accountService.setNewStockList(this.account?.Id ?? "", stockList);
                this.notifyChanged();
            });
        
        this.dtOptions = {
            pagingType: 'full_numbers',
            pageLength: 10,
            processing: true
        };

        this.dtTrigger.next(this.account?.StockList);
    }

    public ngOnDestroy(): void {
        this.unsubscribe$.next();
        this.unsubscribe$.complete();
        this.dtTrigger.unsubscribe();
    }

    public get stockList() {
        return this.account?.StockList;
    }

    public logout(): void {
        this.authService.logout();
        this.router.navigate(["/"]);
        this.appState.authState.logout$.next();
    }

    public openAddNewStockListModal(): void {
        this.modalRef = this.modalService.open(AddNewStockListComponent);
    }

    public openEditStockListModal(index: number): void {
        const editingStockList = this.account?.StockList[index];
        this.modalRef = this.modalService.open(EditStockListComponent, {
            data: { stockList: editingStockList },
            modalClass: "modal-lg"
          });
    }

    public removeStockList(stockListIndex: number): void {
        if (stockListIndex > -1) {
            this.account?.StockList.splice(stockListIndex, 1);
        }

        this.dtTrigger.next(this.account?.StockList);
        this.notifyChanged();
    }

    public async notifyChanged(): Promise<void> {
        await this.accountService.updateAccount(this.account?.Id ?? "", this.account);
    }

}