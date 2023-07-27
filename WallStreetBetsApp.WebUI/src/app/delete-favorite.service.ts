import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DeleteFavorite } from './delete-favorite';

@Injectable({
  providedIn: 'root'
})
export class DeleteFavoriteService {

  constructor(private http: HttpClient) { }

  deleteFavorite(deleteFavorite: DeleteFavorite, cb: any){
    this.http.delete(`https://localhost:7262/api/WallStreetBets/favorites?username=${deleteFavorite.username}&ticker=${deleteFavorite.ticker}`).subscribe(cb)
  }
}
