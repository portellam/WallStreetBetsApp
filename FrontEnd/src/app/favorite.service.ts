import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Favorite } from './favorite';
import { UserService } from './user.service';

@Injectable({
  providedIn: 'root'
})
export class FavoriteService {

  // METHODS //

  // DEPENDENCIES
  constructor(private _UserService: UserService, private _HttpClient: HttpClient) { }

  // CRUD FUNCTIONS

  //post(ticker: string, cb: any) {
  postFavorite(ticker: string, cb: any) {
    //alert(this._UserService.get())
    this._HttpClient.post(`https://localhost:7262/api/WallStreetBets/favorites?username=${this._UserService.get()}&ticker=${ticker}`, ticker).subscribe(
      (result: any) => {
        console.log('RESULTS FROM SAVING FAVORITE:');
        console.log(result);
        if (result) {
          cb(result.id);
        }
        else {
          cb(null);
        }
      }
    );
  }
}
