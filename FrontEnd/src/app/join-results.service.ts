import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { JoinResults } from './join-results';
import { UserService } from './user.service';

@Injectable({
  providedIn: 'root'
})
export class JoinResultsService {

  constructor(private http: HttpClient, private userService: UserService) { }

  /* COMMENTING OUT TO PRESERVE MOMENTARILY
  getJoinResults(cb: any){
    this.http.get<JoinResults[]>('https://localhost:7262/api/WallStreetBets/joinresults?username=coloritoj').subscribe(cb)
  } */

  getJoinResults(cb: any){
    this.http.get<JoinResults[]>(`https://localhost:7262/api/WallStreetBets/joinresults?username=${this.userService.getCurrent()}`).subscribe(cb)
  }

  // For now, I'm just using my name to see if this is working
  // VERY IMPORTANT: Once we figure out how to pass in a username, remember to use the `` (these are different from '') for the string
  // The `` are found on the ~ key (next to 1)
}
