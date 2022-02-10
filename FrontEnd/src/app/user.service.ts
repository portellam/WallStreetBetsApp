import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from './user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  username: string = '';
  // METHODS //
  constructor(private http: HttpClient) { }
  
  login(username: string) {
    this.username = username;
    alert(this.username);
  }

  getCurrent() {
    return this.username;
  }

  postUser(_User: User, cb: any){
    this.http.post<User>(`https://localhost:7262/api/WallStreetBets?username=${_User.username}&first_name=${_User.first_name}`, _User).subscribe(cb);
  }
}
