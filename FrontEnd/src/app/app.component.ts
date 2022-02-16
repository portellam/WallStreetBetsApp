import { Component, Input } from '@angular/core';
import { UserService } from './user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'FrontEnd';

  // PROPERTIES //
  // User
  _firstName: string = '';
  _username: string = '';

  // TOGGLES
  _userVisible: boolean = false;

  // ========== //
  // METHODS //
  constructor(private _UserService: UserService) {
  }

  getLogin(){
    this._userVisible = this._UserService.getLogin();
  }

  getUser(){
    this._username = this._UserService.get().username;
    this._firstName = this._UserService.get().first_name;
  }
}
