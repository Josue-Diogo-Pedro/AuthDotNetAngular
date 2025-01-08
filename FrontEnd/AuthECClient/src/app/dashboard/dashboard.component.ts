import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../shared/services/auth.service';
import { UserService } from '../shared/services/user.service';
import { HideIfClaimsNotMetDirective } from '../shared/hide-if-claims-not-met.directive';
import {claimReq} from '../shared/utils/ClaimReq-utils'

@Component({
  selector: 'app-dashboard',
  imports: [HideIfClaimsNotMetDirective],
  templateUrl: './dashboard.component.html',
  styles: ``
})
export class DashboardComponent implements OnInit{

  constructor(private router:Router,
    private authService: AuthService,
    private userService: UserService
  ){}
  
  fullName: string = '';
  public claimReq = claimReq;

  ngOnInit(): void {
    this.userService.getUserProfile().subscribe({
      next: (res: any) => this.fullName = res.fullName,
      error: (err: any) => console.log('error while retrieving user profile: \n', err)
    });
  }

}
