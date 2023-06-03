import { Component, EventEmitter, Input, Output } from "@angular/core";

@Component({
    selector: "select-ratio",
    templateUrl: "./select-ratio.component.html"
})

export class SelectRatioComponent {
    @Input() public ration!: number;
    @Output() public rationChange = new EventEmitter<number>();
}