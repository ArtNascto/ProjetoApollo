import { Component, Injector, ChangeDetectionStrategy } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Router } from '@angular/router';

@Component({
  templateUrl: './home.component.html',
  animations: [appModuleAnimation()],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class HomeComponent extends AppComponentBase {
  constructor(injector: Injector,private router: Router,) {
    super(injector);
  }
  ngOnInit(){
    this.router.navigate(["/app/questionnaire"]);
  }
}
