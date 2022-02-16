import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DeleteFavorite } from './delete-favorite';

@Injectable({
  providedIn: 'root'
})
export class DeleteFavoriteService {

  // METHODS //

  // DEPENDENCIES
  constructor(private _HttpClient: HttpClient) { }

  // CRUD FUNCTIONS
  
  //delete(_Favorite: DeleteFavorite, cb: any) {
  deleteFavorite(_Favorite: DeleteFavorite, cb: any) {
    this._HttpClient.delete(`https://localhost:7262/api/WallStreetBets/favorites?username=${_Favorite.username}&ticker=${_Favorite.ticker}`).subscribe(cb)
  }
}
