import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../services/admin.service';
import { Router, ActivatedRoute } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Sale, Product, City, SaleFilter } from '../../models/admin.model';

@Component({
  selector: 'app-edit-sale',
  templateUrl: './edit-sale.component.html',
  styleUrls: ['./edit-sale.component.css']
})
export class EditSaleComponent implements OnInit {
  loading = true;
  sale: Sale = {
    cityName: '',
    id: 0,
    price: 0,
    productId: '',
    productName: '',
    userName: ''
  };
  filter: SaleFilter = {
    cityName: '',
    count: 1,
    id: 0,
    maxPrice: 0,
    minPrice: 0,
    page: 0,
    productId: '',
    productName: '',
    userName: ''
  };
  usernames: string[] = [];
  products: Product[] = [];
  cities: City[] = [];
  product = '';

  constructor(private service: AdminService, private snackBar: MatSnackBar,
              private router: Router,
              private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.params.subscribe(p => {
      this.sale.id = +p['id'] || 0;
      if (this.sale.id === 0) {
        this.snackBar.open('Invalid sale Id', 'OK!');
        this.router.navigateByUrl('/Admin/SaleList');
      } else {
        this.service.GetSaleList(this.filter).subscribe(res => {
          this.sale = res[0];
          this.product = this.sale.productId + ' - ' + this.sale.productName;
          this.loading = false;
        });
      }
    });
  }
  Save() {
    const products = this.product.split('-');
    if (products.length < 2)
    {
      this.snackBar.open('Please Select Valid Product','OK!');
    }
    this.sale.productId = products[0].replace(' ', '');
    this.sale.productName = products[1].replace(' ', '');

    this.service.EditSale(this.sale).subscribe(res => {
      this.snackBar.open('Sale Edited Successfully', 'OK!');
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
