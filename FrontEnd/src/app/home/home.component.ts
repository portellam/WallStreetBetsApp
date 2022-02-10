import { Component, OnInit } from '@angular/core';
import { WallStreetBetsInfo } from '../wall-street-bets-info';
import { WallStreetBetsInfoService } from '../wall-street-bets-info.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  wsbArray: WallStreetBetsInfo[] = [];

  constructor(private WallStreetBetsInfoServce: WallStreetBetsInfoService) { }

  ngOnInit(): void {
  }

  showWsbInfo(){
    this.WallStreetBetsInfoServce.retrieveWallStreetBetsInfo(
      (results: any) => {
        this.wsbArray = results;
      }
    );
  }

}
