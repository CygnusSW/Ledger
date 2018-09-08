import { Component, OnInit } from '@angular/core';
import { AngularFontAwesomeComponent } from "angular-font-awesome"
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap"
import * as alertify from "alertify.js";
import { BankingService } from "../banking/banking.service";
import { BankAccount } from '../classes/bank-account';
import { HttpErrorResponse } from '@angular/common/http';
import { FinancialTransaction } from '../classes/financial-transaction';
import { environment } from '../../environments/environment';

@Component({
  selector: 'bl-account-summary',
  templateUrl: './account-summary.component.html',
  styleUrls: ['./account-summary.component..scss']
})
export class AccountSummaryComponent implements OnInit {
  public createTransactionForm : FormGroup;
  public accounts : BankAccount[];
  public destinationAccountNumber : number;
  public destinationAccountName : string;
  public loginErrorMessage : string;

  constructor(
    private _modalService : NgbModal,
    private _fb : FormBuilder,
    private _bankService : BankingService
  ) { }

  ngOnInit() {
    this.createTransactionForm = this._fb.group({
      "transactionType": ["", Validators.required],
      "amount": ["", [Validators.required, Validators.min(0.01)]],
      "accountNumber": [""],
      "description": ["", [Validators.required, Validators.minLength(1)]]
    });

    this._bankService.getAccounts()
    .subscribe(
      (accounts) => {
        this.accounts = accounts;
      }
    )
  }

  openCreateTrxModal(createTrxModal, accountNumber : number, accountName : string)
  {
    this.destinationAccountNumber = accountNumber;
    this.destinationAccountName = accountName;
    this._modalService.open(createTrxModal)
    .result
    .then(
      (_) => {    },
      (_) => {
      }
    )
  }

  createTransaction(trxInfo, createTrxModal)
  {
    var destinationAccount = this.accounts.find(acc => acc.AccountNumber == this.destinationAccountNumber);
    if (destinationAccount)
    {
      this._bankService
        .createTransaction(
          trxInfo.transactionType, 
          trxInfo.amount, 
          trxInfo.description, 
          this.destinationAccountNumber
        )
        .subscribe(
         result => {
            destinationAccount.AccountRecords.push(result);
            destinationAccount.AccountRecords = destinationAccount.AccountRecords.slice();
            alertify.success("Transaction Created!");
            destinationAccount.CurrentBalance += this.getBalanceAffectingAmount(result);
            createTrxModal.close();
            this.createTransactionForm.reset();
         },
         err => alertify.error("Failed to create transaction.")
        );
    }
  }

  getBalanceAffectingAmount(trx : FinancialTransaction)
  {
    debugger;
    var factor = trx.RecordType == environment.creditTypeID  ? 1 : -1;
    return trx.Amount * factor;
  }

}
