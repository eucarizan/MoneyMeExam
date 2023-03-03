import { Component, OnInit } from '@angular/core';
import { QuoteService } from '../quote.service';
import { Options } from '@angular-slider/ngx-slider';
import { FormGroup, FormControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-calculator',
  templateUrl: './calculator.component.html',
  styleUrls: ['./calculator.component.scss']
})
export class CalculatorComponent implements OnInit {

  userForm = new FormGroup({
    first_name: new FormControl(''),
    last_name: new FormControl(''),
    email: new FormControl(''),
    mobile_number: new FormControl(''),
    loan_amount: new FormControl(''),
    affix: new FormControl(''),
    dob: new FormControl({
      year: 2000,
      month: 5,
      day: 16,
    }),
    term: new FormControl('')
  });

  currentLoanId: number = -1;
  currentUserId: number = 0;

  private today = new Date();
  public maxDate = {
    year: this.today.getFullYear(),
    month: this.today.getMonth() + 1,
    day: this.today.getDate()
  };

  constructor(
    private quoteService: QuoteService,
    private route: ActivatedRoute,
    private router: Router
  ) {

  }

  value: number = 5000;

  loanAmountOptions: Options = {
    floor: 2100,
    ceil: 15000
  };

  termValue: number = 1;

  termOptions: Options = {
    floor: 1,
    ceil: 24
  };

  ngOnInit(): void {
    this.currentLoanId = parseInt(this.route.snapshot.paramMap.get('id')!, 10);
    // console.log(a);
    if (this.currentLoanId > 0) {
      this.updateForm();
    } else {
      this.currentLoanId = -1;
    }
  }

  updateForm() {
    this.quoteService.getLoan(this.currentLoanId).subscribe(
      response => {
        this.currentUserId = response.userId;
        let dateObj = new Date(response.dateOfBirth);
        console.log(dateObj);
        
        this.userForm.patchValue({
          first_name: response.firstName,
          last_name: response.lastName,
          email: response.email,
          mobile_number: response.mobile,
          loan_amount: response.amount,
          affix: 'mr',
          dob: {
            year: dateObj.getFullYear(),
            month: dateObj.getMonth(),
            day: dateObj.getDay()
          },
          term: response.term,
        })
      }
    );
  }

  onSubmit() {
    // let date = `${this.userForm.value.dob.year}-${this.userForm.value.dob.month}-${this.userForm.value.dob.day}T00:00:00.000Z`;
    let date = new Date(this.userForm.value.dob.year, this.userForm.value.dob.month - 1, this.userForm.value.dob.day);
    // new Date(new Date(this.userForm.value.dob.year, this.userForm.value.dob.month-1, this.userForm.value.dob.day)).toLocaleString('en', {timeZone: 'Asia/Manila'});
    let dateISO = date.toISOString();

    // debugger
    let quoteParam = {
      "affix": this.userForm.value.affix,
      "firstName": this.userForm.value.first_name,
      "lastName": this.userForm.value.last_name,
      "dateOfBirth": `${dateISO}`,
      "mobile": this.userForm.value.mobile_number,
      "email": this.userForm.value.email,
      "amount": this.userForm.value.loan_amount,
      "term": this.userForm.value.term,
    }
    this.quoteService.saveLoan(quoteParam).subscribe(
      response => {
        // this.router.navigate(['result', response['redirectUrl']])
        // this.router.navigateByUrl(response['redirectUrl'])
        window.location.href = response['redirectUrl']
      },
      error => console.log('error: ', error)
    );
    // console.log("date " + date + "\ndateISO " + dateISO );
  }

  updateDb() {
    let date = new Date(this.userForm.value.dob.year, this.userForm.value.dob.month - 1, this.userForm.value.dob.day);
    let dateISO = date.toISOString();

    let quoteParam = {
      "userId": this.currentUserId,
      "loanId": this.currentLoanId,
      "affix": this.userForm.value.affix,
      "firstName": this.userForm.value.first_name,
      "lastName": this.userForm.value.last_name,
      "dateOfBirth": `${dateISO}`,
      "mobile": this.userForm.value.mobile_number,
      "email": this.userForm.value.email,
      "amount": this.userForm.value.loan_amount,
      "term": this.userForm.value.term,
    }

    this.quoteService.updateLoan(quoteParam).subscribe(
      response => {
        this.router.navigate(['/result', response.loanId]);
      }
    );
  }

}
