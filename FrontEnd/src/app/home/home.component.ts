import { Component, OnInit } from '@angular/core';
import { WallStreetBetsInfo } from '../wall-street-bets-info';
import { WallStreetBetsInfoService } from '../wall-street-bets-info.service';
import { MarketStackService } from '../market-stack.service';
import { MarketStack } from '../market-stack';
import { Favorite } from '../favorite';
import { FavoriteService } from '../favorite.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  wsbArray: WallStreetBetsInfo[] = [];

  marketStackInfo: MarketStack | undefined;

  myFav: Favorite = {
    id: 0,
    ticker: '',
    username: ''
  }

  constructor(private WallStreetBetsInfoService: WallStreetBetsInfoService,
    private MarketStackService: MarketStackService,
    private FavoriteService: FavoriteService) { }

  ngOnInit(): void {
  }

  showWsbInfo(){
    this.WallStreetBetsInfoService.retrieveWallStreetBetsInfo(
      (results: any) => {
        this.wsbArray = results;
      }
    );
  }

  showMarketStackInfo(ticker: string){
    this.MarketStackService.retrieveMarketStackInfo(ticker,
      (results: any) => {
        console.log('STACK INFO:');
        console.log(results);
        this.marketStackInfo = results;
      }
    );
  }


  // OK, this function is working
  // What I need to do is figure out how to pass the ticker they are favoriting into myFav
  // I also need to figure out how to pass their username as well (without having to type it)
  addFav(ticker: string){
    //alert(ticker);
    this.FavoriteService.postFavorite(ticker,
      (result: any) => {
        //alert('Succesfully added favorite!')
      }
    );
  }

}
