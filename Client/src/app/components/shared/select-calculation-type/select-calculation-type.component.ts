import { Component, EventEmitter, Input, Output } from "@angular/core";
import { CalculationType } from "../../models/stock-model/stock-base.model";

@Component({
    selector: "select-calculation-type",
    templateUrl: "./select-calculation-type.component.html"
})

export class SelectCalculationType {
    @Input() public calculationType!: CalculationType;
    @Output() public calculationTypeChange = new EventEmitter<CalculationType>();

    public calculationTypeValue = CalculationType;
}