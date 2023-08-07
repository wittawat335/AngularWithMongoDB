import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { ResponseApi } from '../class/response-api';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class MenuService {
  private urlApi: string = environment.apiBaseUrl + '/Menu';

  constructor(private http: HttpClient) {}

  GetList(userId: number): Observable<ResponseApi> {
    return this.http.get<ResponseApi>(`${this.urlApi}/${userId}`);
  }
}
