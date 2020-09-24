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
  rowsOnPage = 10;
  currentCategory = 1;
  constructor(private rankingService: RankingService) { }

  usersRanking: Array<UserRanking>;

  ngOnInit(): void {
    this.getRankingData(this.rowsOnPage, 0, 1);
  }

  getRankingData(take: number, skip: number, category: number) {
    this.rankingService.getRanking({Take : take, Skip : skip, RankingCategory : category, Order : false}).subscribe( t => {
      this.usersRanking = t;
    });
  }

  getRanking(option: string) {
    if (option === 'more') {
      this.currentPage++;
    } else {
      this.currentPage--;
    }
    this.getRankingData(this.rowsOnPage, this.rowsOnPage * this.currentPage, this.currentCategory);
  }

  canUseLessButton() {

    if (this.usersRanking === null) {
       return false;
      }

    if (this.currentPage > 0) {
      return true;
    }
  }

  canUseMoreButton() {
    if (this.usersRanking === null) {
      return false;
     }

    if (this.usersRanking.length < this.rowsOnPage) {
       return false;
     }

    return true;
  }

  rankingBy(category: number) {

    if (category === this.currentCategory) {
      return;
    }

    this.currentCategory = category;
    this.currentPage = 0;

    this.getRankingData(this.rowsOnPage, this.rowsOnPage * this.currentPage, this.currentCategory);
  }

}
