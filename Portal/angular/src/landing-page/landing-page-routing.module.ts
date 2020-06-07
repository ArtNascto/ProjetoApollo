import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { LandingPageComponent } from "./landing-page.component";
import { RegisterInstitutionComponent } from "./register-institution/register-institution.component";
import { InitialContentComponent } from "./initial-content/initial-content.component";

@NgModule({
  imports: [
    RouterModule.forChild([
      {
        path: '',
        component: LandingPageComponent,
        children: [
          {
            path: 'registerinstitution',
            component: RegisterInstitutionComponent,
          },
          { path: 'initialcontent', component: InitialContentComponent },
        ],
      },
    ]),
  ],
  exports: [RouterModule],
})
export class LandingPageRoutingModule {}
