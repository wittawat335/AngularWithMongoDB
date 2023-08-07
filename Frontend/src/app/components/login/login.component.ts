import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import anime from 'animejs';
import { Login } from 'src/app/class/login';
import { UserService } from 'src/app/services/user.service';
import { UtilityService } from 'src/app/services/utility.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  formLogin: FormGroup;
  checkPassword: boolean = true;
  showLoading: boolean = false;
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private userService: UserService,
    private utService: UtilityService
  ) {
    this.formLogin = this.fb.group({
      email: ['', Validators.email],
      password: ['', Validators.required],
    });
  }
  ngOnInit(): void {
    this.Test();
    this.anime();
  }

  Test() {
    this.userService.TestEnv().subscribe({
      next: (data) => {
        if (data.isSuccess) {
          console.log(data.message);
        }
      },
    });
  }

  LoginUser() {
    this.showLoading = true;
    const req: Login = {
      email: this.formLogin.value.email,
      password: this.formLogin.value.password,
    };

    this.userService.Login(req).subscribe({
      next: (data) => {
        console.log(data);
        if (data.isSuccess) {
          this.utService.setSessionUser(data.value);
          this.router.navigate(['pages']);
        } else {
        }
      },
      complete: () => {
        this.showLoading = false;
      },
      error: () => {},
    });
  }

  //#region Anime
  anime() {
    var current: any = null;
    const test = document.querySelector('#email');
    document.querySelector('#email')!.addEventListener('focus', function (e) {
      if (current) current.pause();
      current = anime({
        targets: 'path',
        strokeDashoffset: {
          value: 0,
          duration: 700,
          easing: 'easeOutQuart',
        },
        strokeDasharray: {
          value: '240 1386',
          duration: 700,
          easing: 'easeOutQuart',
        },
      });
    });
    document
      .querySelector('#password')!
      .addEventListener('focus', function (e) {
        if (current) current.pause();
        current = anime({
          targets: 'path',
          strokeDashoffset: {
            value: -336,
            duration: 700,
            easing: 'easeOutQuart',
          },
          strokeDasharray: {
            value: '240 1386',
            duration: 700,
            easing: 'easeOutQuart',
          },
        });
      });
    document.querySelector('#submit')!.addEventListener('focus', function (e) {
      if (current) current.pause();
      current = anime({
        targets: 'path',
        strokeDashoffset: {
          value: -730,
          duration: 700,
          easing: 'easeOutQuart',
        },
        strokeDasharray: {
          value: '530 1386',
          duration: 700,
          easing: 'easeOutQuart',
        },
      });
    });
  } ////
  //#endregion
}
