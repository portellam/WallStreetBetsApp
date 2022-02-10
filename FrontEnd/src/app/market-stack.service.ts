import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MarketStack } from './market-stack';
import { StockInfo } from './stock-info';

@Injectable({
  providedIn: 'root'
})
export class MarketStackService {

  constructor(private http: HttpClient) { }

  retrieveMarketStackInfo(cb: any) {
    this.http.get<MarketStack>('https://localhost:7262/api/WallStreetBets/marketstack').subscribe(cb);
  }
}
