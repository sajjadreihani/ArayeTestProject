import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  MainInfo = false;
  Report = false;
  collpaseAll() {
    this.MainInfo = false;
    this.Report = false;
  }

  constructor() { }

  ngOnInit() {
  }

}
