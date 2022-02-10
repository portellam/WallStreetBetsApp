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
  // METHODS //
  constructor(private UserService: UserService, private router:Router) {}

  ngOnInit(): void {
  }

  addToList(){
    console.log('addToList');
    console.log(this._User);
    this.UserService.postUser(this._User, 
      (result: any) => {
        // ALERT: server doesn't return anything right now
        alert('Success!');
        // this.router.navigate(['/']);  // TODO: you can redirect to another component from here
      }
      );
  }
}
