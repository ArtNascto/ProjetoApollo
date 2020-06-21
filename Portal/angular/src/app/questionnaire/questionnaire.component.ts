import {
  Component,
  Injector,
  ChangeDetectionStrategy,
  OnInit,
  ViewEncapsulation,
} from "@angular/core";
import { AppComponentBase } from "@shared/app-component-base";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import {
  InstitutionData,
  Institution,
  Questionary,
} from "./institution-data.component";
import { NgSelectConfig } from "@ng-select/ng-select";
import {
  ClientDto,
  ApolloServiceProxy,
} from "@shared/service-proxies/service-proxies";
import * as mapboxgl from 'mapbox-gl';
import { environment } from "environments/environment";
@Component({
  templateUrl: "./questionnaire.component.html",
  styleUrls: ["./questionnaire.component.scss"],
  animations: [appModuleAnimation()],
  encapsulation: ViewEncapsulation.None,
})
export class QuestionnaireComponent extends AppComponentBase implements OnInit {
  questionaryProgress: number = 0;
  questionaryProgressLabel: string = "";
  institution: Institution[] = [];
  specialities: string[] = [];
  selectedSpeciality: string = "";
  dropdownSettings = {};
  questionaryResult: Questionary = {
    pain: false,
    quantityPain: 0,
    regionOfPain: [],
    vomit: false,
    breathDificulty: false,
    painOnBreath: false,
    fever: "",
    feverQuantity: null,
    bleeding: false,
    stopBleeding: false,
    nausea: false,
  };
  step: number = 0;
  bodyRegions: string[] = [
    "Cabeça",
    "Olhos",
    "Nariz",
    "Ouvido",
    "Pescoço",
    "Garganta",
    "Coração",
    "Pulmão",
    "Peito",
    "Braço Direito",
    "Braço Esquerdo",
    "Torax",
    "Costas",
    "Barriga",
    "Estomago",
    "Genitais",
    "Perna Esquerda",
    "Perna Direita",
    "Joelho Direito",
    "Joelho Esquerdo",
    "Pé Direito",
    "Pé Esquerdo",
  ];
  map: mapboxgl.Map;
  style = 'mapbox://styles/mapbox/streets-v11';
  zoom = 12
  constructor(
    injector: Injector,
    private _institutionData: InstitutionData,
    private _apolloServiceProxy: ApolloServiceProxy,
    private config: NgSelectConfig
  ) {
    super(injector);
    this.config.notFoundText = "Não encontrado";
    // set the bindValue to global config when you use the same
    // bindValue in most of the place.
    // You can also override bindValue for the specified template
    // by defining `bindValue` as property
    // Eg : <ng-select bindValue="some-new-value"></ng-select>
    this.config.bindValue = "value";
    mapboxgl.accessToken = environment.mapbox.accessToken;
  }

  ngOnInit() {
    this._apolloServiceProxy.getClient().subscribe(c => {
      this.initQuestionary(c.medicalInsurance);
    });
  }

  initQuestionary(medicalInsurance:string) {
    this.questionaryProgress = parseFloat((100 / 3).toFixed(0));
    this.questionaryProgressLabel = this.questionaryProgress + " %";
    this.institution = this._institutionData.institution;

    if (
      medicalInsurance !== null &&
      medicalInsurance !== ""
    ) {
      this.institution = this.institution.filter((elem) => {
        return (
          elem.type === "public" ||
          elem.medicalInsurance.includes(medicalInsurance)
        );
      });
    }
    for (var i = 0; i < this.institution.length; i++) {
      if (this.institution[i].specialities) {
        this.specialities = this.specialities.concat(
          this.institution[i].specialities
        );
      }
    }
    this.specialities = this.specialities
      .filter(function (elem, index, self) {
        return index === self.indexOf(elem);
      })
      .sort();

    this.step = 1;
    console.log(this.step)
  }
  secondPart() {
    this.questionaryProgress = parseFloat(((100 / 3) *2).toFixed(0));
    this.questionaryProgressLabel = this.questionaryProgress + " %";
    this.step = 2;
  }
  getMap() {
    this.questionaryProgress = parseFloat(((100 / 3 )*3).toFixed(0));

    console.log(this.questionaryResult);
    this.step = 3;
    this.map = new mapboxgl.Map({
      container: 'map',
      style: this.style,
      zoom: this.zoom,
      // center: [this.lng, this.lat]
    })
    this.map.addControl(new mapboxgl.NavigationControl());
  }
  finalize() {}
}
