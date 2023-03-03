import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { QuoteService } from '../quote.service';
import { NgbAlertConfig } from '@ng-bootstrap/ng-bootstrap';
import { debounceTime } from 'rxjs/operators';
import { NgbAlert, NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-result',
  templateUrl: './result.component.html',
  styleUrls: ['./result.component.scss']
})
export class ResultComponent implements OnInit {

  details = {
    firstName: '',
    lastName: '',
    email: '',
    repaymentAmount: 0,
    term: 0,
    amount: 0,
    mobile: 0,
    userId: '',
    weeklyRepayment: 0,
    totalRepayment: 0,
    paymentInterest: 0,
    loanId: '',
  }
  isSuccess: Boolean =  false;

  constructor(
   private route: ActivatedRoute,
   private quoteService: QuoteService,
   alertConfig: NgbAlertConfig
  ) {
    alertConfig.type = 'success';
    alertConfig.dismissible = false;
   }

  ngOnInit(): void {
    const loanId = parseInt(this.route.snapshot.paramMap.get('id')!, 10);
    this.quoteService.getLoan(loanId).subscribe(
      response => {
        this.details = response;
        this.details['weeklyRepayment'] = this.details.repaymentAmount/4.34524;
        this.details['paymentInterest'] = this.details.amount * .065;
        this.details['totalRepayment'] = this.details.amount + 300 + this.details.paymentInterest;
        console.log(response);
      }
    );
  }

  confirm(): void {
    this.isSuccess = true;
  }

}
