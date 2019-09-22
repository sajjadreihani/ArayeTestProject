import { Component, OnInit } from '@angular/core';
import { Sale, SaleFilter } from '../../models/admin.model';
import { AdminService } from '../../services/admin.service';
import { MatSnackBar } from '@angular/material/snack-bar';
declare var $: any;

@Component({
  selector: 'app-sale-list',
  templateUrl: './sale-list.component.html',
  styleUrls: ['./sale-list.component.css']
})
export class SaleListComponent implements OnInit {
  loading = true;
  sale: Sale[] = [];
  saleId = 0;
  filter: SaleFilter = {
    cityName: '',
    count: 10,
    id: 0,
    maxPrice: 0,
    minPrice: 0,
    page: 0,
    productId: '',
    productName: '',
    userName: ''
  };

  constructor(private service: AdminService, private snackBar: MatSnackBar) {}

  ngOnInit() {
    this.LoadData();
  }
  Remove() {
    this.service
      .RemoveSale(this.sale.filter(s => s.id === this.saleId)[0])
      .subscribe(
        res => {
          $('#exampleModalCenter').modal('hide');

          this.snackBar.open('Sale Removed Successfully', 'OK!');
          this.LoadData();
        },
        err => {
          $('#exampleModalCenter').modal('hide');

          this.snackBar.open('Something Wrong! Please Try Later', 'OK!');
        }
      );
  }
  LoadData() {
    this.service.GetSaleList(this.filter).subscribe(res => {
      this.sale = res;
      this.loading = false;
    });
  }
}
