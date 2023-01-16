import { Component, Input, OnInit } from "@angular/core";
import { CandlestickData, ColorType, createChart, WhitespaceData } from "lightweight-charts"
import { SecurityService } from "src/app/services/securitys.service";

@Component({
    selector: "security-chat",
    templateUrl: "./security-chart.component.html",
    styleUrls: ["./security-chart.component.scss"]
})

export class SecurityChatComponent implements OnInit {
    @Input() secid?: string;

    constructor(public securityService: SecurityService) {

    }

    public async ngOnInit(): Promise<void> {
        const chartOptions = {
            width: 1200,
            height: 600,
            layout: {
                textColor: 'black', 
                background: { 
                    type: ColorType.Solid, 
                    color: 'white' 
                } 
                } 
            };

        const chart = createChart(document.getElementById('chart-container') as HTMLElement, chartOptions);
        const candlestickSeries = chart.addCandlestickSeries({ 
                upColor: '#26a69a', 
                downColor: '#ef5350', 
                borderVisible: false, 
                wickUpColor: '#26a69a', 
                wickDownColor: '#ef5350' 
            });
 
        let data: (CandlestickData | WhitespaceData)[] = [];
        (await this.securityService.getSecurityChartData(this.secid ?? "")).forEach(item => {
            data.push(
                {
                    open: item.Open,
                    high: item.Hight,
                    low: item.Low,
                    close: item.Close,
                    time: item.Time
                }
            )
        });

        candlestickSeries.setData(data);
        chart.timeScale().fitContent();
    }
}

