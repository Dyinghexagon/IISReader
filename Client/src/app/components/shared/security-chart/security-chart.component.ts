import { Component, Input, OnInit } from "@angular/core";
import { ColorType, createChart } from "lightweight-charts"

@Component({
    selector: "security-chat",
    templateUrl: "./security-chart.component.html",
    styleUrls: ["./security-chart.component.scss"]
})

export class SecurityChatComponent implements OnInit {
    @Input() secid: string = "";

    constructor() {

    }

    public ngOnInit(): void {
        const chartOptions = {
            width: 1200,
            height: 400,
            layout: {
                textColor: 'black', 
                background: { 
                    type: ColorType.Solid, 
                    color: 'white' 
                } 
                } 
            };

        const chart = createChart(document.getElementById('chart-div') as HTMLElement, chartOptions);
        const candlestickSeries = chart.addCandlestickSeries({ 
                upColor: '#26a69a', 
                downColor: '#ef5350', 
                borderVisible: false, 
                wickUpColor: '#26a69a', 
                wickDownColor: '#ef5350' 
            });
        
        const data = [
            { open: 10, high: 10.63, low: 9.49, close: 9.55, time: '2018-12-12' },
            { open: 9.55, high: 10.30, low: 9.42, close: 9.94, time: '2018-12-13' },
            { open: 9.94, high: 10.17, low: 9.92, close: 9.78, time: '2018-12-14' }, 
            { open: 9.78, high: 10.59, low: 9.18, close: 9.51, time: '2018-12-15' }, 
            { open: 9.51, high: 10.46, low: 9.10, close: 10.17, time: '2018-12-16' }, 
            { open: 10.17, high: 10.96, low: 10.16, close: 10.47, time: '2018-12-17' }, 
            { open: 10.47, high: 11.39, low: 10.40, close: 10.81, time: '2018-12-18' }, 
            { open: 10.81, high: 11.60, low: 10.30, close: 10.75, time: '2018-12-19' }, 
            { open: 10.75, high: 11.60, low: 10.49, close: 10.93, time: '2018-12-20' }, 
            { open: 10.93, high: 11.53, low: 10.76, close: 10.96, time: '2018-12-21' }
        ];

        candlestickSeries.setData(data);
        
        chart.timeScale().fitContent();

        console.warn("draw chart!");
    }
}