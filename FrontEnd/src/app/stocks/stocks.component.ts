import { Component, OnInit, Input} from '@angular/core';

declare const TradingView: any;

@Component({
  selector: 'app-stocks',
  templateUrl: './stocks.component.html',
  styleUrls: ['./stocks.component.css']
})
export class StocksComponent implements OnInit {

  get stock(): string {
    return this._stock;
  }

  @Input() set stock(newStock: string) {
    this._stock = newStock;
    this.createWidget(newStock);
    console.log(newStock);
  }

  _stock: string = '';

  constructor() { }

  ngOnInit(): void {
  }

  ngAfterViewInit() {
    this.createWidget("TSLA");
  }

  createWidget(newStock: string) {
    new TradingView.widget(
      {
        "width": 1200,
        "height": 400,
        "symbol": `${newStock}`,
        "intervial": "D",
        "timezone": "Etc/UTC",
        "theme": "light",
        "style": "1",
        "locale": "en",
        "toolbar_bg": "#f1f3f6",
        "enable_publishing": false,
        "allow_symbol_change": true,
        "container_id": "tradingview_3fba1"
      }
    );
  }

}
