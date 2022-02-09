import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from './user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  // METHODS //
  constructor(private http: HttpClient) { }

  // CRUD FUNCTIONS
  // URL: https://localhost:7262/api/WallStreetBets


  // NOTE: not necessary for end users, maybe admin user?
  getUsers(cb: any){
    this.http.get<User[]>('https://localhost:7262/api/WallStreetBets').subscribe(cb);
  }

  postUser(_User: User, cb: any){
    this.http.post<User>(`https://localhost:7262/api/WallStreetBets?username=${_User.username}&first_name=${_User.first_name}`, _User).subscribe(cb);
  }

  putUser(_User: User, cb: any){
    this.http.post<User>(`https://localhost:7262/api/WallStreetBets?username=${_User.username}&first_name=${_User.first_name}`, _User).subscribe(cb);
  }

  deleteUser(_User: User, cb: any){
    this.http.post<User>(`https://localhost:7262/api/WallStreetBets?user_id=${_User.id}`, _User).subscribe(cb);
  }

}
