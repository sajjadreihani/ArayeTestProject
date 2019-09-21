export interface Sale {
  id: number;
  cityName: string;
  userName: string;
  productName: string;
  productId: string;
  price: number;
}
export interface SaleFilter {
  productName: string;
  productId: string;
  maxPrice: number;
  minPrice: number;
  cityName: string;
  userName: string;
  page: number;
  count: number;
  id: number;
}
export interface City {
  Id: number;
  name: string;
}
export interface Product {
  productId: string;
  productName: string;
}
