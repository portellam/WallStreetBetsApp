import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { WallStreetBetsInfo } from './wall-street-bets-info';

@Injectable({
  providedIn: 'root'
})
export class WallStreetBetsInfoService {

  // METHODS //
  
  // DEPENENCIES
  constructor(private _HttpClient: HttpClient) { }

  // CRUD FUNCTIONS
  //get(cb: any) {
  retrieveWallStreetBetsInfo(cb: any) {
    this._HttpClient.get<WallStreetBetsInfo[]>('https://localhost:7262/api/WallStreetBets/nbshare').subscribe(cb);
  }
}
