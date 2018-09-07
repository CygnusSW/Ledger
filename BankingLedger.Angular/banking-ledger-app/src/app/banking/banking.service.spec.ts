import { TestBed, inject } from '@angular/core/testing';

import { BankingService } from './banking.service';
import { HttpTestingController, HttpClientTestingModule } from "@angular/common/http/testing";
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

describe('Banking Service', () => {
  let httpClient: HttpClient;
  let httpTestingController: HttpTestingController;


  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [BankingService],
      imports: [
        HttpClientTestingModule
      ]
    });

    // Inject the http service and test controller for each test
    httpClient = TestBed.get(HttpClient);
    httpTestingController = TestBed.get(HttpTestingController);
  });


  describe("Creating New Bank Accounts", () => {
    const defaultAccountName = "Test123";
    const defaultAccountNumber = 12857373;

    it("should send an http request to the 'account' endpoint.", inject([BankingService], (service: BankingService) => {
      service.CreateAccount(defaultAccountName)
        .subscribe(res => { });

      var req = httpTestingController.expectOne(`${environment.apiRoot}/api/v1/account`);
    }));

    it("should include the account name in the request.", inject([BankingService], (service: BankingService) => {
      service.CreateAccount(defaultAccountName)
        .subscribe(res => { });

      var req = httpTestingController.expectOne(`${environment.apiRoot}/api/v1/account`);
      expect(req.request.body["AccountName"]).toEqual(defaultAccountName);
    }));

    it("should return an observable with the created account, including the accound number and account name", inject([BankingService], (service: BankingService) => {

      service.CreateAccount(defaultAccountName)
        .subscribe(
          (res: any) => {
            expect(res.AccountName).toEqual(defaultAccountName);
            expect(res.AccountNumber).toBeDefined();
          }
        );


      var req = httpTestingController.expectOne(`${environment.apiRoot}/api/v1/account`);
      req.flush({
        AccountName: defaultAccountName,
        AccountNumber: defaultAccountNumber
      });

    }));
  })


});
