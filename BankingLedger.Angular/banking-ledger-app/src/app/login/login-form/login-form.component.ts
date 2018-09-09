import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators, FormGroupName} from '@angular/forms';
import { AuthService } from '../../auth/auth.service';
import { Subscriber } from 'rxjs';
import { registerContentQuery } from '@angular/core/src/render3/instructions';
import { NgbModal, ModalDismissReasons } from "@ng-bootstrap/ng-bootstrap";
import { AngularFontAwesomeModule} from "angular-font-awesome";
import { Router } from "@angular/router";
import * as alertify from "alertify.js";
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'bl-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component..scss']
})
export class LoginFormComponent implements OnInit {
  public loginForm : FormGroup;
  public registrationForm : FormGroup;
  public displayRegistrationResults : boolean= false;
  public successfullyRegistered : boolean = false;
  public registrationResultMessage : string;
  public validationErrors : string[] = [];
  public loginErrorMessage : string;

  constructor(
    private _fb : FormBuilder,
    private _authService : AuthService,
    private _modalService : NgbModal,
    private _router : Router
  ) { }

  ngOnInit() {
    this.loginForm = this._fb.group({
      "username": ["", Validators.required],
      "password": ["", Validators.required]
    });

    this.registrationForm = this._fb.group({
      "username": ["", Validators.required],
      "password": ["", Validators.required, Validators.minLength(6)],
      "confirmPassword": ["", Validators.required],
      "accountName": ["", Validators.required]
    })
  }

  login(username : string, password : string)
  {
    this._authService.Login(username, password)
    .subscribe(
      (authResult) => {
        debugger;
        this.navigateToSummaryPage();
      },
      (err : HttpErrorResponse) => {
        debugger;
        if (err.status == 400)
          alertify.alert("Invalid username or password.");
        else 
          alertify.alert("Unable to contact banking ledger");
      }
    );
  }

  navigateToSummaryPage()
  {
    this._router.navigate(["summary"]);
  }

  register(registrationModal : any, registrationForm : any)
  {
    //TODO Pull this comparison into a validator
    if (registrationForm.password != registrationForm.confirmPassword)
    {
      this.displayRegistrationMessage("Passwords must match.", false);
      return;
    }

    this._authService.Register(registrationForm.username, registrationForm.password, registrationForm.accountName)
    .subscribe(
      (response) => {
        if (response != null)
        {
          alertify.success("Created account!");
          registrationModal.close();
        }
        else 
        {
          this.displayRegistrationMessage("Username not available.", false);
        }
      },
      (err) => {
        this.displayRegistrationMessage("Unable to create new account. Please try again later.", false);
      }
    );
  }

  displayRegistrationMessage(message : string, isSuccess: boolean)
  {
    this.registrationResultMessage = message;
    this.displayRegistrationResults = true;
    this.successfullyRegistered = isSuccess;
  }

  openRegistrationModal(modal)
  {
    this._modalService.open(modal).result
    .then(
      (res) => {
        this.resetRegistrationForm();
      },
      (cancel) => {
        this.resetRegistrationForm();
      }

    );
  }

  resetRegistrationForm()
  {
    this.registrationForm.reset();
    this.registrationResultMessage = "";
    this.displayRegistrationResults = false;
    this.successfullyRegistered = false;
  }


}
