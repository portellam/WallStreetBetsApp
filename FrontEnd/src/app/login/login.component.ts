import { ThisReceiver } from '@angular/compiler';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../user';
import { UserService } from '../user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  // PROPERTIES //
  _User: User = {
    id: 0,
    username: '',
    first_name: ''
  }
  Users: User[] = [];

  // OUTPUTS
  @Output() theUser: EventEmitter<User> = new EventEmitter<User>();

  // METHODS //
  constructor(private UserService: UserService, private router:Router) {}

  ngOnInit(): void {
  }

  // NOTE: name changes? refreshList for User sign in? existing user?
  refreshList() {
    this.UserService.getUsers(
      (results: any) => {
        this.Users = results;
      }
    )
  }

  addToList(){
    console.log('addToList');
    console.log(this._User);
    this.UserService.postUser(this._User, 
      (result: any) => {
        // ALERT: server doesn't return anything right now
        //alert('Success!');
        // this.router.navigate(['/']);  // TODO: you can redirect to another component from here

      }
      );
  }

  isUserTaken(){}

  // EMITTERS
  emitUser(){
    this.theUser.emit(this._User);
    // clear the fields
    //this._User.id = 0;
    //this._User.username = '';
    //this._User.first_name = '';
  }

  

}
