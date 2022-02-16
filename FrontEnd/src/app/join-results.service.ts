import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { JoinResults } from './join-results';
import { UserService } from './user.service';

@Injectable({
  providedIn: 'root'
})
export class JoinResultsService {

  // METHODS //

  // DEPENDENCIES
  constructor(private _HttpClient: HttpClient, private _UserService: UserService) { }

  // CRUD FUNCTIONS

  /* COMMENTING OUT TO PRESERVE MOMENTARILY
  get(cb: any) {
    this._HttpClient.get<JoinResults[]>('https://localhost:7262/api/WallStreetBets/joinresults?username=coloritoj').subscribe(cb)
  } */

  get(cb: any) {
  //getJoinResults(cb: any) {
    this._HttpClient.get<JoinResults[]>(`https://localhost:7262/api/WallStreetBets/joinresults?username=${this._UserService.get()}`).subscribe(cb)
  }

  // NOTE:
  // For now, I'm just using my name to see if this is working
  // VERY IMPORTANT: Once we figure out how to pass in a username, remember to use the `` (these are different from '') for the string
  // The `` are found on the ~ key (next to 1)
}
