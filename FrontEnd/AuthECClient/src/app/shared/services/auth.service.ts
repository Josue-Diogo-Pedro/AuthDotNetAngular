import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TOKEN_KEY } from '../constants';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }

  createUser(formData: any){
    return this.http.post(environment.apiBaseUrl+'/Auth/signup', formData);
  }

  signin(formData:any){
    return this.http.post(environment.apiBaseUrl+'/Auth/signin',formData);
  }

  isLoggedIn(){
    return this.getToken() != null? true : false;
  }

  saveToken(token: string){
    localStorage.setItem(TOKEN_KEY, token);
  }

  getToken(){
    return localStorage.getItem(TOKEN_KEY);
  }

  deleteToken(){
    localStorage.removeItem(TOKEN_KEY);
  }

  getClaims(){
    return JSON.parse(window.atob(this.getToken()!.split('.')[1]));
  }
}
