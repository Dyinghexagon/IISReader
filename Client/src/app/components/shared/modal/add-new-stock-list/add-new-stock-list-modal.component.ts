import { Component, } from "@angular/core";
import { UntypedFormControl, UntypedFormGroup, Validators } from "@angular/forms";
import { Guid } from "guid-typescript";
import { MdbModalRef } from "mdb-angular-ui-kit/modal";
import { ModalState } from "src/app/components/models/modal-state.module";
import { IStockListModel } from "src/app/components/models/stock-list.model";
import { CalculationType } from "src/app/components/models/stock-model/stock-base.model";

@Component({
  selector: "add-new-stock-list-modal",
  templateUrl: "add-new-stock-list-modal.component.html",
  styleUrls: ["add-new-stock-list-modal.component.scss"]
})

export class AddNewStockListComponent {
  public newStockListForm: UntypedFormGroup;

  constructor(
    private readonly state: ModalState,
    public modalRef: MdbModalRef<AddNewStockListComponent>
  ) {
    this.newStockListForm = new UntypedFormGroup({
      title: new UntypedFormControl("", [Validators.minLength(1), Validators.maxLength(100)]),
      calculationType: new UntypedFormControl("", [Validators.required])
    });
  }

  public close(): void {
    this.modalRef.close();
  }

  public async submitForm(): Promise<void> {
    this.state.addNewStockList.createdStockList$.next({
      id: Guid.create().toString(),
      title: this.newStockListForm.get("title")?.value,
      stocks: [],
      isNotificated: true,
      calculationType: this.newStockListForm.get("calculationType")?.value as CalculationType
    });
  }
}
