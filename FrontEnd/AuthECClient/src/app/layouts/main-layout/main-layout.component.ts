import { Component } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { AuthService } from '../../shared/services/auth.service';
import { HideIfClaimsNotMetDirective } from '../../shared/hide-if-claims-not-met.directive';
import {claimReq} from '../../shared/utils/ClaimReq-utils'

@Component({
  selector: 'app-main-layout',
  imports: [RouterOutlet, RouterLink, HideIfClaimsNotMetDirective],
  templateUrl: './main-layout.component.html',
  styles: ``
})
export class MainLayoutComponent {

  constructor(
    private authService: AuthService,
    private router: Router
  ){}

  public claimReq = claimReq;

  onLogout(){
    this.authService.deleteToken();
    this.router.navigateByUrl('/signin');
  }
}
