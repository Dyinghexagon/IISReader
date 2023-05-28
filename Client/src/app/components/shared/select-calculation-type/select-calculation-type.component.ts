import { Component, EventEmitter, Input, Output } from "@angular/core";
import { CalculationType } from "../../models/stock-model/stock-base.model";

@Component({
    selector: "select-calculation-type",
    templateUrl: "./select-calculation-type.component.html"
})

export class SelectCalculationType {
    @Input() public calculationType!: CalculationType;
    @Output() public calculationTypeChange = new EventEmitter<CalculationType>();

    public enumValues: CalculationType[] = [
        CalculationType.Arifmetic,
        CalculationType.Hormonic,
        CalculationType.Square
    ];

    public labesl: ISelectCalculationTypeLabel[] = [
        {
            Name: "Среднее арифметическое",
            Type: CalculationType.Arifmetic
        },
        {
            Name: "Среднее гармоническое",
            Type: CalculationType.Hormonic
        },
        {
            Name: "Среднее квадратическое",
            Type: CalculationType.Square
        }
    ]

}

export interface ISelectCalculationTypeLabel {
    Name: string;
    Type: CalculationType;
}