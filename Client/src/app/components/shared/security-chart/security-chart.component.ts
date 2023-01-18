import { Component, Input, OnInit } from "@angular/core";
import { CandlestickData, ColorType, createChart, IChartApi, ISeriesApi } from "lightweight-charts"
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
 
        let data: CandlestickData[] = [];
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
        
        this.addLegend(candlestickSeries, data, chart);

        chart.timeScale().fitContent();
    }

    private addLegend(candlestickSeries: ISeriesApi<"Candlestick">, data: CandlestickData[], chart: IChartApi): void {
        const container = document.getElementById("chart-container");
        let legend = document.getElementById("legend") as HTMLDivElement;

        container?.append(legend);
        
        const getLastBar = () => data[data.length - 1];
        const buildDateString = (time: any) => `${time.year} - ${time.month} - ${time.day}`;
        const formatPrice = (price: any) => (Math.round(price * 100) / 100).toFixed(2);
        const setTooltipHtml = (name: any, date: any, price: any) => {
            legend.innerHTML = `
                <div style="font-size: 24px; margin: 10px 0px;">${name}</div>
                <div style="font-size: 22px; margin: 10px 0px;">${price}</div>
                <div>${date}</div>`;
        };
        
        const updateLegend = (param: any) => {
            const validCrosshairPoint = !(
                param === undefined || param.time === undefined || param.point.x < 0 || param.point.y < 0
            );
            const bar = validCrosshairPoint ? param : getLastBar();
            const time = bar.time;
            const date = buildDateString(time);
            let price;
            try {
                price = param.seriesPrices.get(candlestickSeries).open;
            } catch {
                price = getLastBar().open;
            }
            const formattedPrice = formatPrice(price);
            setTooltipHtml(this.secid, date, formattedPrice);
        };
        
        chart.subscribeCrosshairMove(updateLegend);
        
        updateLegend(undefined);
    }

}

