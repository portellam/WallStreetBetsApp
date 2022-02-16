import { ThisReceiver } from '@angular/compiler';
import { Component, OnInit, Output } from '@angular/core';
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
  // User
  Users: User[] = [];
  _User: User = {
    id: 0,
    username: '',
    first_name: ''
  }
  _firstName: string = '';
  _username: string = '';

  // TOGGLES
  _userVisible: boolean = this._UserService.userVisible;
  signUpVisible: boolean = false;
  deleteVisible: boolean = false;
  editVisible: boolean = false;
  // ========== //

  // METHODS //
  constructor(private _UserService: UserService, private _Router:Router) {
    this.getAll();
  }
  ngOnInit(): void {
  }

  // CRUD FUNCTIONS
  getAll() {
    this._UserService.getAll(
      (result: User[]) => {
        this.Users = result;
      }
    );
  }

  get() {
    this._User = this._UserService.get();
  }

  post() {
    this._UserService.post(this._User, this.Users);
  }

  put() {
    this._UserService.post(this._User, this.Users);
  }

  // TODO: looks good, add CRUD to BackEnd Controller from alexbranch 
  /*
  delete() {
    this._UserService.delete(this._User);
  }
  */
  
  // post
  add() {
    // refresh List
    this.get();
    // add to List
    this.post();
  };

  // put
  edit() {
    // refresh List
    this.getAll();
    // sort List for username match, if true exit now.
    for(var i: number = 0; i < this.Users.length; i++)
    {
      while (i != this._User.id)
      {
        if(this._username == this.Users[i].username)
        {
          alert(`Failure! Username is already taken.`)
          this._username = '';
          return;
        }
        break;
      }
    }
    // no match
    alert(`"${this._username}" is available.`);
    // edit User
    this._User.username = this._username;
    this._User.first_name = this._firstName;
    // put
    this.put();
    alert(`Success! User updated.`)
  }

  // delete
  deleteThis() {
    // refresh List
    this.getAll();
    // sort List for username match, if true exit now.
    for(var i: number = 0; i < this.Users.length; i++)
    {
      while (i != this._User.id)
      {
        if(this._username == this.Users[i].username)
        {
          alert(`Failure! Username is already taken.`)
          this._username = '';
          return;
        }
        break;
      }
    }
    // no match
    alert(`"${this._username}" is available.`);
    // edit User
    this._User.username = this._username;
    this._User.first_name = this._firstName;
    // put
    this.put();
    alert(`Success! User updated.`)
  }
  // ========== //

  // FUNCTIONS

  // function checks if user exists, and signs in existing user.
  signIn() {
    // refresh List
    this.getAll();
    // sort List for match. If true, sign-in success.
    for(var i: number = 0; i < this.Users.length; i++)
    {
      if(this._username == this.Users[i].username)
      {
        this.login(true);
        alert(`Success! Login complete.`)
        // auto re-direct
        this._Router.navigate(['/']);
        return;
      }
    }
    //this._userVisible = false;
    alert(`Failure! Username does not exist.`)
  }

  // functions signs out user.
  signOut() {
    this.login(false);
    alert(`User signed out. Good bye.`)
    // auto re-direct
    //this._Router.navigate(['/']); // NOTE: leave as is.
    return;
  }

  // function checks if user does NOT exist, and signs up new user.
  signUp() {
    // refresh List
    this.getAll();
    // sort List for match. If true, sign-up failure.
    for(var i: number = 0; i < this.Users.length; i++)
    {
      if(this._User.username == this.Users[i].username)
      {
        alert(`Failure! Username is already taken.`)
        this._User.username = '';
        return;
      }
    }
    // add User
    this.add();
    alert(`Success! Registration complete.`)
    // auto sign-in
    this.signIn();
  }

  // HOLD FUNCTIONS
  login(bool: boolean) {
    this._UserService.login(bool);
  }

  // TOGGLES
  toggleEdit() {
    if(this.deleteVisible){
      this.toggleDelete();
    }
    this.editVisible = !this.editVisible;
  }

  toggleDelete() {
    
    if(this.editVisible){
      this.toggleEdit();
    }
    this.deleteVisible = !this.deleteVisible;
  }

  toggleSignUp() {
      this.signUpVisible = !this.signUpVisible;
  }
  // ========== //
}
