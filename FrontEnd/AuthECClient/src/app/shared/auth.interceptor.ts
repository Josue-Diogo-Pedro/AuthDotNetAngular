import { HttpInterceptorFn } from '@angular/common/http';
import { AuthService } from './services/auth.service';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { tap } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

export const authInterceptor: HttpInterceptorFn = (req, next) => {

  const authService = inject(AuthService)
  const router = inject(Router)
  const toastr = inject(ToastrService)

  if(authService.isLoggedIn()){
    const cloneReq = req.clone({
      headers: req.headers.set('Authorization', 'Bearer '+ authService.getToken())
    })
    return next(cloneReq).pipe(
      tap({
        error:(err:any) =>{
          if(err.status == 401){
            authService.deleteToken();
            setTimeout(() => {
              toastr.info('Please Login Again', 'Session Expired!')
            }, 1500);
            router.navigateByUrl('/signin')
          }
          else if(err.status == 403)
            toastr.error('Opps! It seams that you are not authorization to perform the action')
        }
      })
    );
  }
  else
    return next(req);
};
