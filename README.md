# Cygnus Banking Ledger #
A basic project build out from .Net framework 4.6.1, with an Angular front-end. This project was built green-field.

### Supported Features: ###
 * Create an login and bank account
 * Create a credit transaction
 * Create a debit transaction
 * View current account balance
 * log out

### Solution Layout ###
The solution is comprised of four projects:
* BankingLedger.API -- All Identity code, including the custom stores is included in this project, along with the endpoints used by the Angular project.
* BankingLedger.Angular -- The front-end project built out of Angular 6+. 
* BankingLedger.Core -- This project contains the core ledger-related logic.
* BankingLedger.Core.Test -- This project contains the unit tests for the core ledger project.

### Pre-Reqs ###
* Angular CLI v6.1.2+
* Node v9.1+

### Up and Running ###
To run this project, pull this repository with:
`git clone https://github.com/Rom2711/Ledger.git `

Once you have the project locally. Open the solution with Visual Studio and run the solution through debugger or deploy to IIS server. There are pre-build scripts on the API project to automatically build the angular project and serve it through Web Api Endpoints. **This may take several minutes**; All of the npm packages must be installed, as part of the build process.



### Testing ###
To run unit tests, run all of the tests in the VS test explorer or other test-runner. 

### Credits ###
* Loading Video: "https://www.shutterstock.com/video/clip-2540876?irgwc=1&utm_medium=Affiliate&utm_campaign=Oxford%20Media%20Solutions&utm_source=51471&utm_term="
* No-Record Placeholder: "https://www.shutterstock.com/image-vector/do-list-nothing-written-on-line-557208001?src=ICq6mKhMtFivS8mDHD7snA-1-83"
* Logo: "https://hatchful.shopify.com/"
