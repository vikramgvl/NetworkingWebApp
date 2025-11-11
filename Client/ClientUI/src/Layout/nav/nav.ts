import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../../core/services/account-service';
import { tick } from '@angular/core/testing';

@Component({
  selector: 'app-nav',
  imports: [FormsModule],
  templateUrl: './nav.html',
  styleUrl: './nav.css',
})
export class Nav {
  protected accountService = inject(AccountService);
  protected creds: any = {};

  login() {
    this.accountService.login(this.creds).subscribe({
      next: (response) => {
        console.log(response),
        this.creds = {};
      },
      error: (error) => {
        console.error(error);
      },
      complete: () => {
        console.log('completed');
      },
    });
  }

  logout() {
    this.accountService.logout();
  }
}
