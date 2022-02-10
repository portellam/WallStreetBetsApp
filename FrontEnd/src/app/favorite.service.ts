import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Favorite } from './favorite';

@Injectable({
  providedIn: 'root'
})
export class FavoriteService {

  constructor(private http: HttpClient) { }

  postFavorite(_Favorite: Favorite, cb: any){
    this.http.post<Favorite>(`https://localhost:7262/api/WallStreetBets/favorites?username=${_Favorite.username}&ticker=${_Favorite.ticker}`, _Favorite).subscribe(cb);
  }

  // NOTE: This is all still happening under my username
}
