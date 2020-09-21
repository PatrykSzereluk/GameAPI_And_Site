import { Component, OnInit } from '@angular/core';
import {ApplicationRef } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private ref: ApplicationRef) {this.Refresh(); }

  ngOnInit(): void {
  }
  
  Refresh() {
    this.ref.tick();
 }
}
