import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../services/admin.service';
import { AdminappComponent } from '../../adminapp.component';

@Component({
  selector: 'app-admin-header',
  templateUrl: './admin-header.component.html',
  styleUrls: ['./admin-header.component.css']
})
export class AdminHeaderComponent implements OnInit {
  fullscreen: boolean = false;

  constructor(private service: AdminService, private main: AdminappComponent) { }

  ngOnInit() {
  }
  Logout() {
    this.service.LogOut().subscribe(res => {
      this.main.signedIn = false;
    });
  }
  goFullScreen() {
    if (this.fullscreen) {
      if (document['exitFullscreen']) {
        document['exitFullscreen']();
      } else if (document['mozCancelFullScreen']) {
        /* Firefox */
        document['mozCancelFullScreen']();
      } else if (document['webkitExitFullscreen']) {
        /* Chrome, Safari and Opera */
        document['webkitExitFullscreen']();
      } else if (document['msExitFullscreen']) {
        /* IE/Edge */
        document['msExitFullscreen']();
      }
    }
    else {
      if (document.documentElement['requestFullscreen']) {
        document.documentElement['requestFullscreen']();
      } else if (document.documentElement['mozRequestFullScreen']) {
        /* Firefox */
        document.documentElement['mozRequestFullScreen']();
      } else if (document.documentElement['webkitRequestFullscreen']) {
        /* Chrome, Safari and Opera */
        document.documentElement['webkitRequestFullscreen']();
      } else if (document['msRequestFullscreen']) {
        /* IE/Edge */
        document.documentElement['msRequestFullscreen']();
      }
    }
    this.fullscreen = !this.fullscreen;
  }

}
