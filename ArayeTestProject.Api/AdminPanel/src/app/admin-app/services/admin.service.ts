import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient, HttpRequest } from '@angular/common/http';
import { Login, News, ShowMessage } from '../models/admin.model';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};


@Injectable()
export class AdminService {

  constructor(private http: HttpClient) { }
  loggedIn() {
    return this.http.get('/api/admin/loggedin');
  }

  Login(login: Login) {
    let body = JSON.stringify(login);
    return this.http.post('/api/main/login', body, httpOptions)
  }

  LogOut() {
    return this.http.post('/api/main/logout', '', httpOptions)
  }

  ChangePassword(password) {
    let body = JSON.stringify({ 'password': password });
    return this.http.post('/api/admin/ChangePassword?password=' + password, '', httpOptions)
  }
  UploadData(file) {
    const formData: FormData = new FormData();
    console.log(file);
    formData.append('dataFile', file, file.name);
    const uploadReq = new HttpRequest('POST', `/api/admin/RenewData`, formData);
    return this.http.request(uploadReq);
  }

  CreateNews(News: News) {
    let body = JSON.stringify(News);
    return this.http.post('/api/admin/CreateNews', body, httpOptions)
  }
  EditNews(news: News) {
    let body = JSON.stringify(news);
    return this.http.post('/api/admin/EditNews', body, httpOptions)
  }
  RemoveNews(newsId) {
    return this.http.post('/api/admin/RemoveNews/' + newsId, '', httpOptions)
  }
  GetNewsList() {
    return this.http.get<News[]>('/api/admin/NewsList')
  }
  GetNews(newsId) {
    return this.http.get<News>('/api/admin/News/' + newsId)
  }
  GetMessageList() {
    return this.http.get<ShowMessage[]>('/api/admin/Messages')
  }
  RemoveMessage(mesId) {
    return this.http.post('/api/admin/RemoveMessage/' + mesId, '', httpOptions)
  }

}
