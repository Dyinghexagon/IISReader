import { Component, Input, OnInit } from "@angular/core";
import { UntypedFormControl, UntypedFormGroup, Validators } from "@angular/forms";
import { Guid } from "guid-typescript";
import { MdbModalRef } from "mdb-angular-ui-kit/modal";
import { ModalState } from "src/app/components/models/modal-state.module";
import { IStockListModel } from "src/app/components/models/stock-list.model";
import { CalculationType } from "src/app/components/models/stock-model/stock-base.model";

@Component({
  selector: "edit-stock-list-modal",
  templateUrl: "./edit-stock-list-modal.component.html",
  styleUrls: ["./edit-stock-list-modal.component.scss"]
})

export class EditStockListModalComponent implements OnInit {

  @Input() public stockList!: IStockListModel;

  public editStockListForm: UntypedFormGroup;
  public calculationType!: CalculationType;
  public ratio!: number;

  constructor(
    private readonly state: ModalState,
    public modalRef: MdbModalRef<EditStockListModalComponent>
  ) {
    this.editStockListForm = new UntypedFormGroup({
      title: new UntypedFormControl("", [Validators.minLength(1), Validators.maxLength(100)])
    });
  }

  public ngOnInit(): void {
    this.calculationType = this.stockList.CalculationType ?? CalculationType.Arifmetic;
    this.ratio = this.stockList.Ratio ?? 2;
  }

  public close(): void {
    this.modalRef.close();
  }

  public async submitForm(): Promise<void> {
    const title = this.editStockListForm.get("title")?.value as string;
    this.state.stockListState.editedStockList$.next({
      Id: this.stockList.Id,
      Title: title.length === 0 ? this.stockList.Title : title,
      Stocks: this.stockList.Stocks ?? [],
      IsNotificated: this.stockList.IsNotificated ?? true,
      CalculationType: this.calculationType,
      Ratio: this.ratio
    });
    this.close();
  }
}
