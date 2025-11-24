import { Component, OnInit, Signal } from '@angular/core';
import { Account } from '../../../core/services/account';
import { User } from '../../models/User';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-header',
  imports: [RouterLink, RouterLinkActive],
  templateUrl: './header.html',
  styleUrl: './header.css',
})
export class Header {
  public account: Signal<User | null>;
  public constructor(public accountService: Account, private router: Router) {
    this.account = this.accountService.currentUser;
  }

  logout() {
    this.accountService.logOut().subscribe({
      next: () => {
        this.accountService.currentUser.set(null);
        this.router.navigateByUrl('/');
      }
    })
  }


}
