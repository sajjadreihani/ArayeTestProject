import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../services/admin.service';
import { Router, ActivatedRoute } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { News } from '../../models/admin.model';

@Component({
  selector: 'app-edit-news',
  templateUrl: './edit-news.component.html',
  styleUrls: ['./edit-news.component.css']
})
export class EditNewsComponent implements OnInit {
  loading: boolean = true;
  news: News = {
    content: '',
    created: '',
    id: 0,
    title: ''
  };

  constructor(private service: AdminService, private snackBar: MatSnackBar
    , private router: Router,
      private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.params.subscribe(p => {
      this.news.id = +p['id'] || 0;
      if (this.news.id == 0) {
        this.snackBar.open('Invalid News Id', 'OK!');
        this.router.navigateByUrl('/Admin/NewsList');
      }
      else {
        this.service.GetNews(this.news.id).subscribe(res => {
          this.news = res;
          this.loading = false;
        });
      }
    });
  }
  Save() {
    this.service.EditNews(this.news).subscribe(res => {
      this.snackBar.open('News Edited Successfully', 'OK!');
      this.router.navigateByUrl('/Admin/NewsList');

    }, err => {
      this.snackBar.open('Something Wrong! Please Try Later', 'OK!');

    })

  }
}
