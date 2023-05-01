import { Component, Input } from "@angular/core";
import { UntypedFormControl, UntypedFormGroup, Validators } from "@angular/forms";
import { Guid } from "guid-typescript";
import { MdbModalRef } from "mdb-angular-ui-kit/modal";
import { ModalState } from "src/app/components/models/modal-state.module";
import { StockListModel } from "src/app/components/models/stock-list.model";

@Component({
    selector: "edit-stock-list-modal",
    templateUrl: "./edit-stock-list.component.html",
    styleUrls: [ "./edit-stock-list.component.scss" ]
})

export class EditStockListComponent {

    @Input() public stockList!: StockListModel;

    public editStockListForm: UntypedFormGroup;
        
    constructor(
        private readonly state: ModalState,
        public modalRef: MdbModalRef<EditStockListComponent>
    ) {
        this.editStockListForm = new UntypedFormGroup({
            title: new UntypedFormControl("", [Validators.minLength(1), Validators.maxLength(100)]),
        });
    }

    public close(): void {
        this.modalRef.close();
    }

    public async submitForm(): Promise<void> {
        const newNewStockList = new StockListModel({
            id: Guid.create().toString(),
            title: this.editStockListForm.get("title")?.value,
            stocks: [],
            isNotificated: true
        });

        this.state.addNewStockList.editedStockList$.next(newNewStockList);
    }
}