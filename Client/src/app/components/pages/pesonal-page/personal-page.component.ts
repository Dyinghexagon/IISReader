import { Component, OnInit  } from "@angular/core";
import { Router } from "@angular/router";
import { MdbModalRef, MdbModalService } from "mdb-angular-ui-kit/modal";
import { AuthService } from "src/app/services/auth.service";
import { AccountModel } from "../../models/account.model";
import { AppState } from "../../models/app-state.module";
import { ModalState } from "../../models/modal-state.module";
import { StockListModel } from "../../models/stock-list.model";
import { AddNewStockListComponent } from "../../shared/modal/add-new-stock-list/add-new-stock-list-modal.component";

@Component({
    selector: "personal-page",
    templateUrl: "./personal-page.component.html",
    styleUrls: [ "./personal-page.component.scss" ]
})

export class PersonalPageComponent implements OnInit {

    public account!: AccountModel | null;
    public tab: "planning" | "notification" = "planning";
    public modalRef: MdbModalRef<AddNewStockListComponent> | null = null;

    constructor(
        private readonly authService: AuthService,
        private readonly modalService: MdbModalService,
        private readonly router: Router,
        private readonly appState: AppState,
        private readonly modalState: ModalState
    ) {
    }

    public async ngOnInit(): Promise<void>  {
        this.account = await this.appState.getAccount();

        this.modalState.addNewStockList.createdList$.subscribe((stockList: StockListModel) => {
            console.warn(stockList);
            this.account?.stockLists?.push(stockList);

            console.warn(this.account?.stockLists);
        });
    }

    public logout(): void {
        this.authService.logout();
        this.router.navigate(["/"]);
        this.appState.authState.logout$.next();
    }

    public openModal(): void {
        this.modalRef = this.modalService.open(AddNewStockListComponent);
    }

}
