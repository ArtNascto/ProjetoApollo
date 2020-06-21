import { Component, Injector, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { finalize } from "rxjs/operators";
import { AppComponentBase } from "@shared/app-component-base";
import {
  AccountServiceProxy,
  RegisterInput,
  RegisterOutput,
  ApolloServiceProxy,
} from "@shared/service-proxies/service-proxies";
import { accountModuleAnimation } from "@shared/animations/routerTransition";
import { AppAuthService } from "@shared/auth/app-auth.service";

@Component({
  templateUrl: "./register.component.html",
  animations: [accountModuleAnimation()],
})
export class RegisterComponent extends AppComponentBase implements OnInit {
  model: RegisterInput = new RegisterInput();
  saving = false;
  hasMedicalInsurances:boolean = false;
  medicalInsurances: string[] = [];
  constructor(
    injector: Injector,
    private _accountService: AccountServiceProxy,
    private _router: Router,
    private authService: AppAuthService,
    private _apolloServiceProxy: ApolloServiceProxy
  ) {
    super(injector);
  }
  ngOnInit() {
    this._apolloServiceProxy.getMedicalInsurances().subscribe((resp) => {
      this.medicalInsurances = resp;
    });
  }
  save(): void {
    this.saving = true;
    this._apolloServiceProxy
      .registerClient(this.model)
      .pipe(
        finalize(() => {
          this.saving = false;
        })
      )
      .subscribe((result: RegisterOutput) => {
        if (!result.canLogin) {
          this.notify.success(this.l("SuccessfullyRegistered"));
          this._router.navigate(["/login"]);
          return;
        }

        // Autheticate
        this.saving = true;
        this.authService.authenticateModel.userNameOrEmailAddress = this.model.userName;
        this.authService.authenticateModel.password = this.model.password;
        this.authService.authenticate(() => {
          this.saving = false;
        });
      });
  }
}
