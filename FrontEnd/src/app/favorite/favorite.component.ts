import { Component, OnInit } from '@angular/core';
import { JoinResults } from '../join-results';
import { JoinResultsService } from '../join-results.service';

@Component({
  selector: 'app-favorite',
  templateUrl: './favorite.component.html',
  styleUrls: ['./favorite.component.css']
})
export class FavoriteComponent implements OnInit {

  allJoinResultsForUser: JoinResults[] = [];



  constructor(private JoinResultsService: JoinResultsService) { }

  ngOnInit(): void {
    this.getUserJoinResults();
  }

  getUserJoinResults() {
    this.JoinResultsService.getJoinResults(
      (results: any) => {
        this.allJoinResultsForUser = results;
      }
    );
  }
  
  addNewNote(){
    
  }
  
}
