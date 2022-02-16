import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MarketStack } from './market-stack';
import { StockInfo } from './stock-info';

@Injectable({
  providedIn: 'root'
})
export class MarketStackService {

  // METHODS //

  // DEPENDENCIES
  constructor(private _HttpClient: HttpClient) { }

  // CRUD FUNCTIONS
  
  get(ticker: string, cb: any) {
  //retrieveMarketStackInfo(ticker: string, cb: any) {
    this._HttpClient.get<MarketStack>(
      `https://localhost:7262/api/WallStreetBets/marketstack?ticker=${ticker}`)
      .subscribe(cb);
  }
}
