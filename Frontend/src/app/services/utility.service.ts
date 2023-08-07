import { Injectable } from '@angular/core';
import { Session } from '../class/session';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class UtilityService {
  constructor() {}

  setSessionUser(session: Session) {
    localStorage.setItem(environment.keyLocalAuth, session.accessToken);
    localStorage.setItem('user', JSON.stringify(session));
  }

  getSessionUser() {
    const data = localStorage.getItem('user');
    const user = JSON.parse(data!);
    return user;
  }

  removeSessionUser() {
    localStorage.removeItem('user');
    localStorage.removeItem(environment.keyLocalAuth);
  }
}
