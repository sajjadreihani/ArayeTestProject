import { Component, OnInit } from '@angular/core';
import { News } from '../../models/admin.model';
import { AdminService } from '../../services/admin.service';
import { MatSnackBar } from '@angular/material/snack-bar';
declare var $: any;

@Component({
  selector: 'app-news-list',
  templateUrl: './news-list.component.html',
  styleUrls: ['./news-list.component.css']
})
export class NewsListComponent implements OnInit {
  loading: boolean = true;
  news: News[] = [];
  newsId = 0;
  constructor(private service: AdminService, private snackBar: MatSnackBar) { }

  ngOnInit() {
    this.LoadData();
  }
  Remove() {
    this.service.RemoveNews(this.newsId).subscribe(res => {
      $('#exampleModalCenter').modal('hide');

      this.snackBar.open('News Removed Successfully', 'OK!');
      this.LoadData();

    }, err => {
        $('#exampleModalCenter').modal('hide');

      this.snackBar.open('Something Wrong! Please Try Later', 'OK!');

    })
  }
  LoadData() {
    this.service.GetNewsList().subscribe(res => {
      this.news = res
      this.loading = false;
    });
  }
}
