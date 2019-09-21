import { Component, OnInit } from '@angular/core';
import { Sale, Product, City } from '../../models/admin.model';
import { AdminService } from '../../services/admin.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-news',
  templateUrl: './add-news.component.html',
  styleUrls: ['./add-news.component.css']
})
export class AddSaleComponent implements OnInit {
  sale: Sale = {
    cityName: '',
    id: 0,
    price: 0,
    productId: '',
    productName: '',
    userName: ''
  };
  usernames: string[] = [];
  products: Product[] = [];
  cities: City[] = [];
  product = '';
  constructor(private service: AdminService, private snackBar: MatSnackBar, private router: Router) { }

  ngOnInit() {
  }
  Save() {
    this.service.CreateSale(this.sale).subscribe(res => {
      this.router.navigateByUrl('/Admin/SaleList');

    }, err => {
      this.snackBar.open(err.body, 'OK!');

    });
  }
  UserChange() {
    this.service.GetUserNameList(this.sale.userName).subscribe(res => {
      this.usernames = res;
    });
  }
  ProductChange() {
    this.service.GetProductNameList(this.sale.productName).subscribe(res => {
      this.products = res;
    });
  }
  CityChange() {
    this.service.GetCityNameList(this.sale.cityName).subscribe(res => {
      this.cities = res;
    });
  }
}
