import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { BankAccount } from "../classes/bank-account";
import { FinancialTransaction } from '../classes/financial-transaction';

@Injectable({
  providedIn: 'root'
})
export class BankingService {

  constructor(
    private _http : HttpClient
  ) { }

  public CreateAccount(accountName : string)
  {
    return this._http.post(`${environment.apiRoot}/api/v1/account`, {
      AccountName: accountName
    });
  }

  public getAccounts()
  {
    return this._http.get<BankAccount[]>(`${environment.apiRoot}/api/v1/accounts`);
  }

  public createTransaction(trxType, amount : number, description : string, accountNumber : number)
  {
    return this._http.post<FinancialTransaction>(`${environment.apiRoot}/api/v1/transaction`,
    {
      TransactionType: +trxType,
      Amount: +amount,
      Description: description,
      AccountNumber: +accountNumber
    });
  }

}
