import { Injector, OnInit, Component } from "@angular/core";
import { AppComponentBase } from "@shared/app-component-base";
import { Router } from "@angular/router";

@Component({
  templateUrl: "./initial-content.component.html",
})
export class InitialContentComponent extends AppComponentBase
  implements OnInit {
  constructor(private router: Router, injector: Injector) {
    super(injector);
  }

  ngOnInit(): void {}
  goToLoginPage() {
    this.router.navigate(["app/home"]);
  }
  goToRegisterPage() {}
  searchInstitution() {}
  registerInstitution() {
    this.router.navigate(["landingpage/registerinstitution"]);
  }
}
