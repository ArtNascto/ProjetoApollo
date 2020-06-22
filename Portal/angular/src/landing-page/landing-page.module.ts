import { NgModule } from "@angular/core";
import { ServiceProxyModule } from "@shared/service-proxies/service-proxy.module";
import { LandingPageRoutingModule } from "./landing-page-routing.module";
import { RegisterInstitutionComponent } from "./register-institution/register-institution.component";
import { SharedModule } from "@shared/shared.module";
import { HttpClientModule, HttpClientJsonpModule } from "@angular/common/http";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { InitialContentComponent } from "./initial-content/initial-content.component";
import { LandingPageComponent } from "./landing-page.component";
import { NgxPaginationModule } from "ngx-pagination";
import { CreditCardDirectivesModule } from 'angular-cc-library';
import { ChartsModule } from 'ng2-charts';
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
// import { RecaptchaModule, RecaptchaFormsModule } from 'ng-recaptcha';
@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    HttpClientJsonpModule,
    SharedModule,
    ReactiveFormsModule,
    LandingPageRoutingModule,
    ServiceProxyModule,
    NgxPaginationModule,
    CreditCardDirectivesModule,
    ChartsModule,
    // RecaptchaModule,
    // RecaptchaFormsModule,
  ],
  declarations: [
    RegisterInstitutionComponent,
    InitialContentComponent,
    LandingPageComponent,
  ],
})
export class LandingPageModule {}
