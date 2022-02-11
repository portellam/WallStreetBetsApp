import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Favorite } from './favorite';
import { UserService } from './user.service';

@Injectable({
  providedIn: 'root'
})
export class FavoriteService {

  constructor(private userService: UserService, private http: HttpClient) { }

  postFavorite(ticker: string, cb: any){
    //alert(this.userService.getCurrent())

    this.http.post(
      `https://localhost:7262/api/WallStreetBets/favorites?username=${this.userService.getCurrent()}&ticker=${ticker}`, ticker).subscribe(
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

  // NOTE: This is all still happening under my username
}
