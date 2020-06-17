import { Component, Injector, ChangeDetectionStrategy, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { environment } from "../../environments/environment";

@Component({
  templateUrl: './questionnaire.component.html',
  styleUrls:['./questionnaire.component.css'],
  animations: [appModuleAnimation()],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class QuestionnaireComponent extends AppComponentBase implements OnInit {

  questionaryProgress:number;
  questionaryProgressLabel:string;

  constructor(injector: Injector) {
    super(injector);
  }

  ngOnInit() {
    this.questionaryProgress = 100
    this.questionaryProgressLabel = this.questionaryProgress+'%';
    }
}
