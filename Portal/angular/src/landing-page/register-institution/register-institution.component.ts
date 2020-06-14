import { Injector, OnInit, Component } from "@angular/core";
import { AppComponentBase } from "@shared/app-component-base";
import { Router } from "@angular/router";
import {
  InstitutionDto,
  ApolloServiceProxy,
  CreateInstitutionInput,
} from "@shared/service-proxies/service-proxies";
import { FormGroup, Validators, FormBuilder } from "@angular/forms";
import { CreditCardValidators } from "angular-cc-library";
@Component({
  selector: "register",
  templateUrl: "./register-institution.component.html",
  styleUrls: ["./register-institution.component.css"],
})
export class RegisterInstitutionComponent extends AppComponentBase
  implements OnInit {
  institutionInfoForm: FormGroup;
  creditCardForm: FormGroup;
  institution: InstitutionDto;
  step: number[] = [];
  securityCode: any;
  siteKey:string = "6LcrX6QZAAAAAGoDKxEomS4T6021nLYM0pnsrZL-";  
  recaptchaValidated:boolean = false;
  constructor(
    private router: Router,
    injector: Injector,
    private _fb: FormBuilder,
    private _apolloServiceProxy: ApolloServiceProxy
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.institutionInfoForm = this._fb.group({
      name: ["", Validators.minLength(3)],
      cnpj: [
        "",
        Validators.pattern("[0-9]{2}.?[0-9]{3}.?[0-9]{3}/?[0-9]{4}-?[0-9]{2}"),
      ],
      technicalContactName: ["", Validators.minLength(3)],
      technicalContactEmail: ["", Validators.email],
      technicalcontactNumber: [
        "",
        Validators.pattern("^\\(?\\d{2}\\)?[\\s-]?[\\s9]?\\d{4}-?\\d{4}$"),
      ],
      addressCep: ["", Validators.pattern("^\\d{5}[-]\\d{3}$")],
      addressAddressLine: ["", Validators.minLength(5)],
      addressNumber: ["", Validators.pattern("^\\d*$")],
      addressComplement: [""],
      addressDistrict: ["", Validators.minLength(3)],
      addressCity: ["", Validators.minLength(3)],
      addressState: ["", Validators.minLength(2)],
    });
    this.creditCardForm = this._fb.group({
      accountHolderName: ["", Validators.minLength(3)],
      creditCard: ["", [CreditCardValidators.validateCCNumber]],
      expirationDate: ["", [CreditCardValidators.validateExpDate]],
      cvc: [
        "",
        [Validators.required, Validators.minLength(3), Validators.maxLength(4)],
      ],
    });
    this._apolloServiceProxy.createInstitution().subscribe((r) => {
      this.institution = r;
    });
  }
  goToLoginPage() {
    this.router.navigate(["app/home"]);
  }
  resolved(captchaResponse: string, res) {
    console.log({captchaResponse})
    // this._apolloServiceProxy.validateRecaptcha(token:captchaResponse).subscribe((r) => {
    //   if(r){
    //     this.recaptchaValidated = true
    //   }
    // });
  }
  goToRegisterPage() {}
  searchAddress(address: string) {
    console.log(address);
    //TODO: descomentar quando conta da google estiver OK
    // var geocoder = new google.maps.Geocoder();
    // geocoder.geocode({ address: address }, (r) => {
    //   console.log(r);
    // });
  }
  validateCreditCard(form: FormGroup) {
    if (form.status !== "VALID") {
      abp.message.error(
        "Falha ao validar o cartão de crédito",
        "ERRO"
      );
    } else {
      this.institution.billingInfo.accountHolderName =
        form.value.accountHolderName;
      this.institution.billingInfo.accountNumber = form.value.creditCard;
      this.institution.billingInfo.expiresDate = form.value.expirationDate;
      let input = new CreateInstitutionInput();
      input.institution = this.institution
      this._apolloServiceProxy.registerInstitution(input).subscribe(r=>{
         this.router.navigate(["app/home"]);
      },err=>{
        abp.message.error(err,"ERRO!");
      })
    }
  }

  NextStep(form: FormGroup) {
    if (form.status !== "VALID") {
      abp.message.error(
        "Ainda existem campos vazios/invalidos",
        "ERRO"
      );
    } else {
      const value = form.value;
      this.institution.name = value.name;
      this.institution.cnpj = value.cnpj;
      this.institution.technicalContact.name = value.technicalContactName;
      this.institution.technicalContact.contactNumber =
        value.technicalcontactNumber;
      this.institution.technicalContact.email = value.technicalContactEmail;
      this.institution.addresses[0].addressLine = value.addressAddressLine;
      this.institution.addresses[0].cep = value.addressCep;
      this.institution.addresses[0].city = value.addressCity;
      this.institution.addresses[0].complement = value.addressComplement;
      this.institution.addresses[0].district = value.addressDistrict;
      this.institution.addresses[0].state = value.addressState;
      this.step.push(1);
    }
  }
}
