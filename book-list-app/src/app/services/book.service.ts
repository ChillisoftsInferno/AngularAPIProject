import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Book } from '../book';
import { Observable } from 'rxjs'; //Used for async operations.

@Injectable({
  providedIn: 'root'
})

export class BookService {

  private apiUrl: string = 'http://localhost:5173/api/Book';

  constructor(private httpClient : HttpClient) {}

  getBooks(): Observable<Book[]> {
    return this.httpClient.get<Book[]>(this.apiUrl);
  }

}
