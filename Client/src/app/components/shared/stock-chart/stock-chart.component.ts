import { Component, Input, OnInit } from "@angular/core";
import { CandlestickData, ColorType, createChart, IChartApi, ISeriesApi } from "lightweight-charts"
import { StockService } from "src/app/services/stock.service";
import { IArchiveDataModel } from "../../models/stock-model/archive-data.model";

@Component({
    selector: "stock-chat",
    templateUrl: "./stock-chart.component.html",
    styleUrls: ["./stock-chart.component.scss"]
})

export class StockChatComponent implements OnInit {
    @Input() secid?: string;

    constructor(public securityService: StockService) {}

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
        const archiveData = await this.securityService.getArchiveDataByYear(this.secid ?? "", new Date().getFullYear());
        Object.entries(archiveData).forEach(item => {
            const key = item[0];
            const value = item[1] as IArchiveDataModel;
            data.push({
                open: value.Open,
                close: value.Close,
                high: value.Hight,
                low: value.Low,
                time: key
            });
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

