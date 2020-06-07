import { Injector, OnInit, Component } from "@angular/core";
import { AppComponentBase } from "@shared/app-component-base";
import { Router } from "@angular/router";
import {
  InstitutionDto,
  ApolloServiceProxy,
} from "@shared/service-proxies/service-proxies";

@Component({
  templateUrl: "./register-institution.component.html",
})
export class RegisterInstitutionComponent extends AppComponentBase
  implements OnInit {
  institution: InstitutionDto;
  constructor(
    private router: Router,
    injector: Injector,
    private _apolloServiceProxy: ApolloServiceProxy
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this._apolloServiceProxy.createInstitution().subscribe((r) => {
      this.institution = r;
    });
  }
  goToLoginPage() {
    this.router.navigate(["app/home"]);
  }
  goToRegisterPage() {}
  searchAddress() {}
}
