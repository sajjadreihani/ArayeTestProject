import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../services/admin.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { HttpEventType } from '@angular/common/http';
declare var $: any;
@Component({
  selector: 'app-import-data',
  templateUrl: './import-data.component.html',
  styleUrls: ['./import-data.component.css']
})
export class ImportDataComponent implements OnInit {
  loading = true;
  file: File;
  constructor(private service: AdminService, private snackBar: MatSnackBar) { }

  ngOnInit() {
    this.loading = false;
  }
  upload(file) {
    this.file = file[0];
  }
  Import() {
    if (this.file) {
      this.service.UploadData(this.file).subscribe(res => {
        if (res.type === HttpEventType.Response) {
          $('#exampleModalCenter').modal('hide');
          this.snackBar.open('Data Imported Successfully', 'OK!');
        }
      }, err => {
        $('#exampleModalCenter').modal('hide');
        this.snackBar.open('Something Wrong!Please Try Later', 'OK!');

        });
    } else {
      this.snackBar.open('Please Select A File', 'OK!');
    }
  }
}
