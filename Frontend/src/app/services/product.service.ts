import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { ResponseApi } from '../class/response-api';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private urlApi: string = environment.apiBaseUrl + '/Product';

  constructor(private http: HttpClient) {}

  GetList(): Observable<ResponseApi> {
    return this.http.get<ResponseApi>(this.urlApi);
  }
}
