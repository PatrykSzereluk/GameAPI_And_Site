import { Component, OnInit } from '@angular/core';
import { RankingService } from '../services/ranking.service';
import { UserRanking } from '../Models/Identity/UserRanking';

@Component({
  selector: 'app-ranking',
  templateUrl: './ranking.component.html',
  styleUrls: ['./ranking.component.css']
})
export class RankingComponent implements OnInit {

  constructor(private rankingService: RankingService) { }

  usersRanking: Array<UserRanking>;

  ngOnInit(): void {
    this.getRanking();
  }

  getRanking() {
    this.rankingService.getRanking({Take : 10, Skip : 0, RankingCategory : 1, Order : false}).subscribe( t => {
      this.usersRanking = t;
      console.log(t);
    });
  }


}
