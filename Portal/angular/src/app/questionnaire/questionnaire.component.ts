import {
  Component,
  Injector,
  OnInit,
  ViewEncapsulation,
  AfterViewInit,
} from "@angular/core";
import { AppComponentBase } from "@shared/app-component-base";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import {
  InstitutionData,
  Institution,
  Questionary,
  InstitutionLocation,
} from "./institution-data.component";
import { NgSelectConfig } from "@ng-select/ng-select";
import { ApolloServiceProxy } from "@shared/service-proxies/service-proxies";
import * as tt from "@tomtom-international/web-sdk-maps";
import * as ttServices from "@tomtom-international/web-sdk-services";
import { HereService } from "@shared/map/here-service";
import { AppConsts } from "@shared/AppConsts";
@Component({
  selector: "app-root",
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
  zoom = 10;
  latitude: number;
  longitude: number;
  map: any;
  institutionLocation: InstitutionLocation[] = [];
  selectedInstitutionName: string;
  selectedInstitution: Institution;
  constructor(
    injector: Injector,
    private _here: HereService,
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
  }

  ngOnInit() {
    this._apolloServiceProxy.getClient().subscribe((c) => {
      this.initQuestionary(c.medicalInsurance);
    });
    this.setCurrentLocation();
  }
  private setCurrentLocation() {
    if ("geolocation" in navigator) {
      navigator.geolocation.getCurrentPosition((position) => {
        this.latitude = position.coords.latitude;
        this.longitude = position.coords.longitude;
        this.zoom = 10;
      });
    }
  }
  initQuestionary(medicalInsurance: string) {
    this.questionaryProgress = parseFloat((100 / 3).toFixed(0));
    this.questionaryProgressLabel = this.questionaryProgress + " %";
    this.institution = this._institutionData.institution;

    if (medicalInsurance !== null && medicalInsurance !== "") {
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
  }
  secondPart() {
    this.questionaryProgress = parseFloat(((100 / 3) * 2).toFixed(0));
    this.questionaryProgressLabel = this.questionaryProgress + " %";
    this.filterInstitution();
    this.step = 2;
  }
  getMap() {
    this.step = 3;
    const mapContainer = document.getElementById("map-container");
    var markerContentElement = document.createElement("div");
    markerContentElement.id = "map";

    mapContainer.appendChild(markerContentElement);

    mapContainer.style.resize = "both";
    this.map = tt.map({
      key: "MQY7QJmKYxBnwKBNBUIGqlUKXIgI19xo",
      container: "map",
      style: "tomtom://raster/1/basic-main",
      center: [this.longitude, this.latitude],
      zoom: this.zoom,
    });
    // this.map.addControl(new tt.FullscreenControl());
    this.map.addControl(new tt.NavigationControl());
    this.createMarker(
      "house-user-solid.svg",
      [this.longitude, this.latitude],
      "",
      "#FAF7F2",
      false,
      ""
    );
    for (let j = 0; j < this.institutionLocation.length; j++) {
      this.createMarker(
        "hospital-symbol-solid.svg",
        this.institutionLocation[j].coord,
        this.institutionLocation[j].name,
        "#89BEEF",
        true,
        this.institutionLocation[j].address
      );
    }

    this.questionaryProgress = parseFloat(((100 / 3) * 3).toFixed(0));
  }
  filterInstitution() {
    this.institution = this.institution.filter((i) => {
      return i.specialities.includes(this.selectedSpeciality);
    });
    this.institution.forEach((i) => {
      const self = this;
      let query =
        i.address.addressLine + "," + i.address.city + "," + i.address.state;
      const URL_TO_FETCH = `http://dev.virtualearth.net/REST/v1/Locations/${query}?&key=Ap3U6SbejKwMfu-9wim2R-bSlZ1Tpq0qwSan4HLmKqNNDQcBtantTzr5MzfN6lyU`;
      fetch(URL_TO_FETCH)
        .then(function (response) {
          response.json().then(function (data) {
            var point = data.resourceSets[0].resources[0].point;
            self.institutionLocation = self.institutionLocation.concat({
              name: i.name,
              coord: [point.coordinates[1], point.coordinates[0]],
              address: query,
            });
          });
        })
        .catch(function (err) {
          console.error(err);
        });
    });
  }
  back() {
    this.questionaryProgress = parseFloat((100 / 3 - 100 / 3).toFixed(0));
    this.questionaryProgressLabel = this.questionaryProgress + " %";
    this.step = this.step - 1;
  }
  finalize() {
    const self = this;
    abp.ui.setBusy();
    setTimeout(function () {
      abp.ui.clearBusy();
      const mapContainer = document.getElementById("map-container");
      var d_interno = document.getElementById("map");
      mapContainer.removeChild(d_interno);
      self.selectedInstitution = self.institution.find(
        (i) => i.name === self.selectedInstitutionName
      );
      self.step = 4;
    }, 3000);
  }
  createMarker(
    icon: string,
    position: any[],
    popupText: string,
    color: string,
    isInstitution: boolean,
    address: string
  ) {
    const self = this;
    var markerElement = document.createElement("div");
    markerElement.className = "marker";

    var markerContentElement = document.createElement("div");
    markerContentElement.className = "marker-content";
    markerContentElement.style.backgroundColor = color;
    markerContentElement.id = popupText + " div";

    markerElement.appendChild(markerContentElement);

    var iconElement = document.createElement("div");
    iconElement.className = "marker-icon";
    iconElement.id = popupText;
    iconElement.style.backgroundImage = `url(${AppConsts.appBaseUrl}/assets/img/${icon})`;
    if (isInstitution) {
      iconElement.onclick = function ($event: any) {
        let institutionName = $event.srcElement.id;
        self.selectedInstitutionName = institutionName;
        self.selectedInstitution = self.institution.find(
          (i) => i.name === self.selectedInstitutionName
        );
      };
    }
    markerContentElement.appendChild(iconElement);
    // if (popupText !== "") {
    //   var popup = new tt.Popup({ offset: 30 }).setText(
    //     popupText + " - " + address
    //   );
    // }
    // add marker to map
    new tt.Marker({ element: markerElement, anchor: "bottom" })
      .setLngLat(position)
      // .setPopup(popup)
      .addTo(this.map);
  }
}
