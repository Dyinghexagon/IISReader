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

  constructor(
    private readonly state: ModalState,
    public modalRef: MdbModalRef<EditStockListComponent>
  ) {
    this.editStockListForm = new UntypedFormGroup({
      title: new UntypedFormControl("", [Validators.minLength(1), Validators.maxLength(100)]),
      calculationType: new UntypedFormControl("", [Validators.required])
    });
  }

  public close(): void {
    this.modalRef.close();
  }

  public async submitForm(): Promise<void> {
    this.state.addNewStockList.editedStockList$.next({
      id: Guid.create().toString(),
      title: this.editStockListForm.get("titlw")?.value ?? "Новый список",
      stocks: [],
      isNotificated: true,
      calculationType: this.editStockListForm.get("calculationType")?.value as CalculationType
    });
  }
}
