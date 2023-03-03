import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { debounce, Observable } from 'rxjs';

import { QuoteResponse } from './model/quoteresponse';

@Injectable({
  providedIn: 'root'
})
export class QuoteService {
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  private url = 'https://my-json-server.typicode.com/JSGund/XHR-Fetch-Request-JavaScript/posts';
  private serverUrl = "https://localhost:7062/api/Quotes"

  constructor(private http: HttpClient) {}

  computeLoan() {
    return this.http.get(this.url);
  }

  saveLoan(quoteParam: any): Observable<QuoteResponse> {
    // return this.http.get(this.serverUrl + '/api/quotes?id=1');
    return this.http.post<QuoteResponse>(this.serverUrl, quoteParam, this.httpOptions);
  }

  getLoan(loanId: number): Observable<any> {
    return this.http.get<any>(`${this.serverUrl}?id=${loanId}`);
  }

  updateLoan(quoteParam: any): Observable<any> {
    return this.http.put<any>(this.serverUrl, quoteParam);
  }
}
