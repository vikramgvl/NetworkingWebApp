import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, signal } from '@angular/core';

@Component({
  selector: 'app-root',
  imports: [],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App implements OnInit {
  private http = inject(HttpClient);
  protected title = 'Networking App';
  protected members = signal<any>([]);
  ngOnInit(): void {
    this.http.get('http://localhost:5262/api/members').subscribe({

      next: response => this.members.set(response),
      error: (error) => {
        console.error(error);
      },
      complete: () => {
        console.log('completed');
      },
    });
  }
}
