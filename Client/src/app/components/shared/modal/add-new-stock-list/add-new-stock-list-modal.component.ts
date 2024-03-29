import { Component, } from "@angular/core";
import { UntypedFormControl, UntypedFormGroup, Validators } from "@angular/forms";
import { Guid } from "guid-typescript";
import { MdbModalRef } from "mdb-angular-ui-kit/modal";
import { ModalState } from "src/app/components/models/modal-state.module";
import { CalculationType } from "src/app/components/models/stock-model/stock-base.model";

@Component({
  selector: "add-new-stock-list-modal",
  templateUrl: "add-new-stock-list-modal.component.html",
  styleUrls: ["add-new-stock-list-modal.component.scss"]
})

export class AddNewStockListModalComponent {
  public newStockListForm: UntypedFormGroup;
  public calculationType: CalculationType;
  public ratio: number;

  constructor(
    private readonly state: ModalState,
    public modalRef: MdbModalRef<AddNewStockListModalComponent>
  ) {
    this.newStockListForm = new UntypedFormGroup({
      title: new UntypedFormControl("", [Validators.minLength(1), Validators.maxLength(100)]),
      isAddAllStocks: new UntypedFormControl(false, [Validators.required]), 
    });
    this.calculationType = CalculationType.Arifmetic
    this.ratio = 2;
  }

  public close(): void {
    this.modalRef.close();
  }

  public async submitForm(): Promise<void> {
    this.state.stockListState.createdStockList$.next({
      stockList: {
        Id: Guid.create().toString(),
        Title: this.newStockListForm.get("title")?.value,
        Stocks: [],
        IsNotificated: true,
        CalculationType: this.calculationType,
        Ratio: this.ratio
      },
      isAddAllStocks: this.newStockListForm.get("isAddAllStocks")?.value
    });
    this.close();
  }
}
