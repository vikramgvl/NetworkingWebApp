import { AccountService } from './../core/services/account-service';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, inject, OnInit, signal } from '@angular/core';
import { Nav } from '../Layout/nav/nav';
import { FormsModule } from '@angular/forms';
import { Home } from "../features/home/home";

@Component({
  selector: 'app-root',
  imports: [Nav, FormsModule, Home],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App implements OnInit {
  private http = inject(HttpClient);
  private accountService = inject(AccountService);
  protected title = 'Networking App';
  protected members = signal<any>([]);
  ngOnInit(): void {
    this.getMembers();
    this.setCurrentUser();
  }

  setCurrentUser() {
    const user = localStorage.getItem('user');
    if (!user)   return;
    
    this.accountService.currentUser.set(JSON.parse(user));
  }
  private getMembers(): void {
    this.http.get('http://localhost:5262/api/members').subscribe({
      next: (response) => this.members.set(response),
      error: (error) => {
        console.error(error);
      },
      complete: () => {
        console.log('completed');
      },
    });
  }
}
