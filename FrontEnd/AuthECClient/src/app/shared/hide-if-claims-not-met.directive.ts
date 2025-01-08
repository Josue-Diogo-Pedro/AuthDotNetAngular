import { Directive, ElementRef, Input, OnInit } from '@angular/core';
import { AuthService } from './services/auth.service';

@Directive({
  selector: '[appHideIfClaimsNotMet]'
})
export class HideIfClaimsNotMetDirective implements OnInit {

  @Input("appHideIfClaimsNotMet") calimReq!: Function;

  constructor(
    private authService: AuthService,
    private elementRef: ElementRef
  ) { }

  ngOnInit(): void {
    const claims = this.authService.getClaims();
    if(!this.calimReq(claims))
      this.elementRef.nativeElement.style.display = "none";
  }

}
