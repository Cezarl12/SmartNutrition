import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { Target, User } from '../../shared/models/User';
import { map, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class Account {
  baseUrl = 'https://localhost:7000/api/user/'
  constructor(private http: HttpClient) { }

  currentUser = signal<User | null>(null);

  login(values: any) {
    let params = new HttpParams();
    params = params.append('useCookies', true);
    const options = {
      params: params,
      withCredentials: true
    };
    return this.http.post(this.baseUrl + 'login', values, options)
  }

  getUserInfo() {
    const options = {
      withCredentials: true
    };
    return this.http.get<User>(this.baseUrl + 'profile', options).pipe(
      map(user => {
        this.currentUser.set(user);
        console.log(user);
        return user;
      })
    )
  }

  logOut() {
    const options = {
      withCredentials: true
    };
    this.currentUser.set(null)
    return this.http.post(this.baseUrl + 'logout', {}, options)
  }

  register(register: any) {
    return this.http.post(this.baseUrl + 'register', register)
  }

  updateProfile(userInfo: User) {
    const options = {
      withCredentials: true
    };
    return this.http.put<User>(this.baseUrl + 'update-profile', userInfo, options).pipe(
      tap((newUser) => {
        this.currentUser.set(newUser)
      })
    )
  }

}
