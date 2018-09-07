import { FinancialTransaction } from "./financial-transaction";

export class BankAccount {
    AccountRecords : FinancialTransaction[];
    AccountNumber : number;
    AccountName : string;
    CurrentBalance : number;
}