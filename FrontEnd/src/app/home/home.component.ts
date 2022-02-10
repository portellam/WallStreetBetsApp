import { Component, OnInit } from '@angular/core';
import { WallStreetBetsInfo } from '../wall-street-bets-info';
import { WallStreetBetsInfoService } from '../wall-street-bets-info.service';
import { MarketStackService } from '../market-stack.service';
import { MarketStack } from '../market-stack';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  wsbArray: WallStreetBetsInfo[] = [];
  marketStackInfo: MarketStack | undefined;

  constructor(private WallStreetBetsInfoServce: WallStreetBetsInfoService,
    private MarketStackService: MarketStackService) { }

  ngOnInit(): void {
  }

  showWsbInfo(){
    this.WallStreetBetsInfoServce.retrieveWallStreetBetsInfo(
      (results: any) => {
        this.wsbArray = results;
      }
    );
  }

  showMarketStackInfo(){
    this.MarketStackService.retrieveMarketStackInfo(
      (results: any) => {
        this.marketStackInfo = results;
      }
    );
  }

}
