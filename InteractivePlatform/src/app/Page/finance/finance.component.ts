import { Component } from '@angular/core';
import { FinanceService } from '../../Services/finance.service';
import { NgClass, NgFor, NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { FinanceRequest } from '../../Models/finance-request';
import { HttpClientModule, provideHttpClient, withFetch } from '@angular/common/http';
import { AccountService } from '../../Services/account.service';
import { Route, Router } from '@angular/router';



@Component({
  selector: 'app-finance',
  standalone: true,
  imports: [FormsModule,NgClass,NgFor,NgIf],
  
  templateUrl: './finance.component.html',
  styleUrl: './finance.component.css'
})
export class FinanceComponent {
 
  financeRequests: FinanceRequest[] = [];
  allRequests: FinanceRequest[] = [];
  filteredRequests: FinanceRequest[] = []
  totalPages: number = 0;
  currentPage: number = 1;
  pageSize: number = 4;
  totalPagesArray: number[] = [];
 
  searchQuery: string = '';

  selectedStatus = '';
  rejectedRequests = 0;
  acceptedRequests = 0;
  totalRequests = 0;
   

  constructor(private financeService: FinanceService,private account:AccountService,private router:Router) {}

  ngOnInit(): void {
    this.loadRequests(this.currentPage);
    this.loadAllRequests(); // Load all requests for summary
  }

  loadRequests(page: number): void {
    this.financeService.getPagination(page, this.pageSize)
      .subscribe({
        next: (response) => {
          console.log('Paginated Response:', response);
          this.financeRequests = response.clients || [];
          this.totalPages = response.totalPages || 0;
          this.totalPagesArray = Array.from({ length: this.totalPages }, (_, i) => i + 1);
          this.currentPage = page;
        },
        error: (error) => {
          console.error('Error loading requests:', error);
          this.financeRequests = [];
        }
      });
  }

  loadAllRequests(): void {
    this.financeService.getAllRequests()
      .subscribe({
        next: (response) => {
          console.log('All Requests Response:', response);
          this.allRequests = response;
          this.updateSummary();
        },
        error: (error) => {
          console.error('Error loading all requests:', error);
          this.allRequests = [];
        }
      });
  }

  updateSummary() {
    this.rejectedRequests = this.allRequests.filter(req => req.status === 'غير مؤهل للشروط').length;
    this.acceptedRequests = this.allRequests.filter(req => req.status === 'مؤهل للشروط').length;
    this.totalRequests = this.allRequests.length;
  }
  searchwithNumber() {
    console.log(this.searchQuery)
    if (this.searchQuery) {
      this.filteredRequests = this.financeRequests.filter(request =>
        request.requestNumber.includes(this.searchQuery)
  
      );
      
    } else {
      this.filteredRequests = this.financeRequests;
    }
  }

  

  filterRequests(): void {
    let filtered = this.allRequests;

    if (this.searchQuery) {
      filtered = filtered.filter(request =>
        request.requestNumber.includes(this.searchQuery) ||
        request.paymentAmount.toString().includes(this.searchQuery) ||
        request.paymentPeriod.toString().includes(this.searchQuery) ||
        request.totalProfit.toString().includes(this.searchQuery)
      );
    }

    if (this.selectedStatus) {
      filtered = filtered.filter(request => request.status === this.selectedStatus);
    }

    this.financeRequests = filtered.slice((this.currentPage - 1) * this.pageSize, this.currentPage * this.pageSize);
    this.totalPages = Math.ceil(filtered.length / this.pageSize);
    this.totalPagesArray = Array.from({ length: this.totalPages }, (_, i) => i + 1);
    this.updateSummary();
  }




  handleAction(request: FinanceRequest) {
    // Implement action logic
    console.log('Handle action for request:', request);
  }

  sendAlert() {
    // Implement send alert logic
    console.log('Send alert to users');
  }

  sendSummary() {
    // Implement send summary logic
    console.log('Send summary of requests');
  }
  search() {
    // Implement search logic
    console.log('Search query:', this.searchQuery);
  }

  prevPage(): void {
    if (this.currentPage > 1) {
      this.loadRequests(this.currentPage - 1);
    }
  }

  nextPage(): void {
    if (this.currentPage < this.totalPages) {
      this.loadRequests(this.currentPage + 1);
    }
  }
  logout() {
    
    this.account.logout();
    this.router.navigate(['/login']);
}




}