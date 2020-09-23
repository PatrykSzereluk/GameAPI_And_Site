import { Component, OnInit } from '@angular/core';
import { RankingService } from '../../services/ranking.service';
import { UserRanking } from '../../Models/Identity/UserRanking';

@Component({
  selector: 'app-ranking',
  templateUrl: './ranking.component.html',
  styleUrls: ['./ranking.component.css']
})
export class RankingComponent implements OnInit {

  currentPage = 0;

  constructor(private rankingService: RankingService) { }

  usersRanking: Array<UserRanking>;

  ngOnInit(): void {
    this.getRankingData(2,0);
  }

  getRankingData(take: number, skip: number) {
    this.rankingService.getRanking({Take : take, Skip : skip, RankingCategory : 1, Order : false}).subscribe( t => {
      this.usersRanking = t;
    });
  }

  getRanking(option: string) {
    if (option === 'more') {
      this.currentPage++;
    } else {
      this.currentPage--;
    }
    this.getRankingData(2, 2 * this.currentPage);
  }

}
