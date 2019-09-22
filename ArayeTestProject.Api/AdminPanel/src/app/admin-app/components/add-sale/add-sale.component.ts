import { Component, OnInit } from '@angular/core';
import { Sale, Product, City } from '../../models/admin.model';
import { AdminService } from '../../services/admin.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-add-sale',
  templateUrl: './add-sale.component.html',
  styleUrls: ['./add-sale.component.css']
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
  ss = false;
  constructor(private service: AdminService, private snackBar: MatSnackBar, private router: Router) { }

  ngOnInit() {
  }
  Save() {
    const products = this.product.split('-');
    if (products.length < 2)
    {
      this.snackBar.open('Please Select Valid Product','OK!');
    }
    this.sale.productId = products[0].replace(' ', '');
    this.sale.productName = products[1].replace(' ', '');
    this.service.CreateSale(this.sale).subscribe(res => {
      this.router.navigateByUrl('/Admin/SaleList');

    }, err => {

      this.snackBar.open(err.error.data.errorMessage, 'OK!');

    });
  }
  UserChange() {
    this.service.GetUserNameList(this.sale.userName).subscribe(res => {
      this.usernames = res;
    });
  }
  ProductChange() {
    this.service.GetProductNameList(this.product).subscribe(res => {
      this.products = res;
    });
  }
  CityChange() {
    this.service.GetCityNameList(this.sale.cityName).subscribe(res => {
      this.cities = res;
    });
  }
}
