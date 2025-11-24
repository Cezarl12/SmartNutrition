import { Component, Signal } from '@angular/core';
import { Account } from '../../core/services/account';
import { User } from '../../shared/models/User';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-profile',
  imports: [RouterLink, RouterOutlet, RouterLinkActive],
  templateUrl: './profile.html',
  styleUrl: './profile.css',
})
export class Profile {
  public account: Signal<User | null>;
  public constructor(private accountService: Account) {
    this.account = this.accountService.currentUser;
  }

}
