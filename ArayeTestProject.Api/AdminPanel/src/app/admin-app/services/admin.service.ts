import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient, HttpRequest } from '@angular/common/http';
import { Sale, City, Product, SaleFilter } from '../models/admin.model';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};


@Injectable()
export class AdminService {

  constructor(private http: HttpClient) { }

  UploadData(file) {
    const formData: FormData = new FormData();
    console.log(file);
    formData.append('file', file, file.name);
    const uploadReq = new HttpRequest('POST', `/api/excel/importfile`, formData);
    return this.http.request(uploadReq);
  }

  CreateSale(sale: Sale) {
    const body = JSON.stringify(sale);
    return this.http.post('/api/sale/add', body, httpOptions);
  }
  EditSale(sale: Sale) {
    const body = JSON.stringify(sale);
    return this.http.post('/api/sale/update', body, httpOptions);
  }
  RemoveSale(sale: Sale) {
    const body = JSON.stringify(sale);
    return this.http.post('/api/sale/remove/', body, httpOptions);
  }
  GetSaleList(filter: SaleFilter) {
    const body = JSON.stringify(filter);
    return this.http.post<Sale[]>('/api/sale/getlist', body, httpOptions);
  }

  GetUserNameList(searchKey) {
    return this.http.get<string[]>('/api/search/username?searchKey=' + searchKey);
  }
  GetProductNameList(searchKey) {
    return this.http.get<Product[]>('/api/search/productname?searchKey=' + searchKey);
  }
  GetCityNameList(searchKey) {
    return this.http.get<City[]>('/api/search/cityname?searchKey=' + searchKey);
  }

}
