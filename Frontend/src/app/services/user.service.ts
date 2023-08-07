import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.development';
import { Login } from '../class/login';
import { Observable } from 'rxjs';
import { ResponseApi } from '../class/response-api';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private urlApi: string = environment.apiBaseUrl + '/Authentication';

  constructor(private http: HttpClient) {}

  TestEnv() {
    return this.http.get<ResponseApi>(`${this.urlApi}/TestEnv`);
  }

  Login(req: Login): Observable<ResponseApi> {
    return this.http.post<ResponseApi>(`${this.urlApi}/Login`, req);
  }
}
