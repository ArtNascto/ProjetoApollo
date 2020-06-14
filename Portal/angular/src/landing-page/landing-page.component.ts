import { Injector, OnInit, Component, ViewEncapsulation } from "@angular/core";
import { AppComponentBase } from "@shared/app-component-base";
import { Router } from "@angular/router";

@Component({
  templateUrl: "./landing-page.component.html",
  styleUrls: ["./landing-page.component.scss"],
  encapsulation: ViewEncapsulation.None,
})
export class LandingPageComponent extends AppComponentBase implements OnInit {
  constructor(private router: Router, injector: Injector) {
    super(injector);
  }

  ngOnInit(): void {}
  goToLoginPage(): void {
    this.router.navigate(["app/home"]);
  }
  goToRegisterPage(): void {
    this.router.navigate(["account/register"]);
  }
}
