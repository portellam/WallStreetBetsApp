import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { WallStreetBetsInfo } from './wall-street-bets-info';

@Injectable({
  providedIn: 'root'
})
export class WallStreetBetsInfoService {

  constructor(private http: HttpClient) { }

  retrieveWallStreetBetsInfo(cb: any){
    this.http.get<WallStreetBetsInfo[]>('https://localhost:7262/api/WallStreetBets/nbshare').subscribe(cb);
  }
}
