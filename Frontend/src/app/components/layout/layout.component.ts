import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UtilityService } from 'src/app/services/utility.service';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styles: [],
})
export class LayoutComponent {
  constructor(private router: Router, private utService: UtilityService) {}

  logOut() {
    this.utService.removeSessionUser();
    this.router.navigate(['login']);
  }
}
