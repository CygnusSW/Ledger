import { TestBed, inject } from '@angular/core/testing';

import { AuthService } from './auth.service';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { environment } from '../../environments/environment';

describe('Authentication Service', () => {
  let httpClient: HttpClient;
  let httpTestingController: HttpTestingController;


  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AuthService],
      imports: [ 
        HttpClientTestingModule 
      ]
    });

    // Inject the http service and test controller for each test
    httpClient = TestBed.get(HttpClient);
    httpTestingController = TestBed.get(HttpTestingController);
  });

  describe("Registering New Users", () => {
    
    it("should call the 'register' endpoint with a POST request.", inject([AuthService], (service : AuthService) => {
      service.Register("TestUser123", "TestPassword123!")
      .subscribe( data => {});
  
      const req = httpTestingController.expectOne(`${environment.apiRoot}/api/v1/register`);
      expect(req.request.method).toEqual("POST");  
    }));

    
    it("should provide username and password when calling the 'register' endpoint.", inject([AuthService], (service : AuthService) => {
      service.Register("TestUser123", "TestPassword123!")
      .subscribe( data => {
        console.log(data);      
      });
  
      const req = httpTestingController.expectOne(`${environment.apiRoot}/api/v1/register`);
      expect(req.request.body["Username"]).toEqual("TestUser123");
      expect(req.request.body["Password"]).toEqual("TestPassword123!");   
    }));

  })


describe("Logging in with Username and Password", () => {

  it("should call the 'login' endpoint with a POST request.", inject([AuthService], (service : AuthService) => {
    service.Login("TestUser123", "TestPassword123!")
    .subscribe( data => {});

    const req = httpTestingController.expectOne(`${environment.apiRoot}/api/v1/login`);
    expect(req.request.method).toEqual("POST");  
  }));

  it("should provide username and password when calling the 'login' endpoint.", inject([AuthService], (service : AuthService) => {
    service.Login("TestUser123", "TestPassword123!")
    .subscribe( data => {});

    const req = httpTestingController.expectOne(`${environment.apiRoot}/api/v1/login`);
    expect(req.request.method).toEqual("POST");
    expect(req.request.body["Username"]).toEqual("TestUser123");
    expect(req.request.body["Password"]).toEqual("TestPassword123!");   
  }));

})

describe("Logging Out", () => {

  it("should call the 'logout' endpoint.", inject([AuthService], (service : AuthService) => {
    service.Logout()
    .subscribe(_ => {});

    const req = httpTestingController.expectOne(`${environment.apiRoot}/api/v1/logout`);
    expect(req.request.method).toEqual("POST");
  }))

})








});
