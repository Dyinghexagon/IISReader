import { Component, Input } from "@angular/core";
import { UntypedFormControl, UntypedFormGroup, Validators } from "@angular/forms";
import { Guid } from "guid-typescript";
import { MdbModalRef } from "mdb-angular-ui-kit/modal";
import { ModalState } from "src/app/components/models/modal-state.module";
import { IStockListModel } from "src/app/components/models/stock-list.model";
import { CalculationType } from "src/app/components/models/stock-model/stock-base.model";

@Component({
  selector: "edit-stock-list-modal",
  templateUrl: "./edit-stock-list.component.html",
  styleUrls: ["./edit-stock-list.component.scss"]
})

export class EditStockListComponent {

  @Input() public stockList!: IStockListModel;

  public editStockListForm: UntypedFormGroup;
  public calculationType: CalculationType;

  constructor(
    private readonly state: ModalState,
    public modalRef: MdbModalRef<EditStockListComponent>
  ) {
    this.editStockListForm = new UntypedFormGroup({
      title: new UntypedFormControl("", [Validators.minLength(1), Validators.maxLength(100)])
    });
    this.calculationType = CalculationType.Arifmetic
  }

  public close(): void {
    this.modalRef.close();
  }

  public async submitForm(): Promise<void> {
    this.state.addNewStockList.editedStockList$.next({
      Id: Guid.create().toString(),
      Title: this.editStockListForm.get("titlw")?.value ?? "Новый список",
      Stocks: [],
      IsNotificated: true,
      CalculationType: this.calculationType
    });
  }
}
