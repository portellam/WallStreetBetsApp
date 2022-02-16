import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from './user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  // PROPERTIES //
  _User: User = {
    id: 0,
    username: '',
    first_name: ''
  }

  // TOGGLES
  userVisible: boolean = false;
  // ========== //

  // METHODS //

  // DEPENDENCIES
  constructor(private _HttpClient: HttpClient) {
  }

  // TOGGLES
  login(bool: boolean) {
    if(bool)
    {
      this.userVisible = true;
    }
    this._User.username = '';
    this._User.first_name = '';
    this.userVisible = false;
  }

  // CRUD FUNCTIONS
  getAll(cb: any) {
    this._HttpClient.get<User[]>(`https://localhost:7262/api/WallStreetBets?`).subscribe(cb);
  }

  getLogin(){
    return this.userVisible;
  }

  get() {
    return this._User;
  }

  post(_User: User, cb: any) {
    this._HttpClient.post<User>(`https://localhost:7262/api/WallStreetBets?username=${_User.username}&first_name=${_User.first_name}`, _User).subscribe(cb);
  }

  put(_User: User, cb: any) {
    this._HttpClient.put<User>(`https://localhost:7262/api/WallStreetBets?username=${_User.username}&first_name=${_User.first_name}`, _User).subscribe(cb);
  }

  // TODO: add DeleteUser from alexbranch
  /*
  delete(_User: User, cb: any) {
    this._HttpClient.delete<User>(`https://localhost:7262/api/WallStreetBets?username=${_User.username}&first_name=${_User.first_name}`, _User).subscribe(cb);
  }
  */
  // ========== //
}
