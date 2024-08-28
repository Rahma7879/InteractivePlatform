import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { FinanceRequest } from '../Models/finance-request';

@Injectable({
  providedIn: 'root'
})
export class FinanceService {

  private apiUrl = "https://localhost:7061/api/finance"; 

  constructor(private http: HttpClient) {}

  getPagination(page: number, pageSize: number): Observable<{ clients: FinanceRequest[], totalPages: number }> {
    return this.http.get<{ clients: FinanceRequest[], totalPages: number }>(`${this.apiUrl}/pagination?page=${page}&size=${pageSize}`);
  }
  getAllRequests() {
    return this.http.get<FinanceRequest[]>(this.apiUrl);
  }
}
