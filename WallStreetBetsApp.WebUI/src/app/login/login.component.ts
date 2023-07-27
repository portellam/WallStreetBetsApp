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
  username: string = '';
  // METHODS //
  constructor(private userService: UserService, private router:Router) {}

  ngOnInit(): void {
  }

  login() {
    this.userService.login(this.username);
    this.router.navigate(["/"]);


  }

  addToList(){
    console.log('addToList');
    console.log(this._User);
    this.userService.postUser(this._User, 
      (result: any) => {
        this.userService.login(this._User.username);
        this.router.navigate(["/"]);
        // ALERT: server doesn't return anything right now
        // this.router.navigate(['/']);  // TODO: you can redirect to another component from here
      }
    );
  }

}
