import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  pathAPI = 'http://localhost:8000/api/cars';
  constructor(private http: HttpClient) { }

  getData(api: string) {
    return this.http.get(this.pathAPI + api);
  }

  postData(api: string, body: any) {
    const fullAPI = this.pathAPI + api;
    console.log('Info: fullapi:', fullAPI);

    return this.http.post<any>(fullAPI, JSON.stringify(body), this.header());
  }

  private header() {
    let header = new HttpHeaders({ 'Content-Type': 'application/json' });

    return { headers: header };
  }

  testConnection(url: string) {
    return this.http.get(url);
  }
}
