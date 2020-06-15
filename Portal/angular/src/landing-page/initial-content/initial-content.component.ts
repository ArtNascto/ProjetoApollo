import { Injector, OnInit, Component } from "@angular/core";
import { AppComponentBase } from "@shared/app-component-base";
import { Router } from "@angular/router";
import { ChartDataSets, ChartOptions } from "chart.js";
import { Color, Label } from "ng2-charts";
@Component({
  templateUrl: "./initial-content.component.html",
})
export class InitialContentComponent extends AppComponentBase
  implements OnInit {
  constructor(private router: Router, injector: Injector) {
    super(injector);
  }
  lineChartData: ChartDataSets[] = [
    { data: [85, 72, 78, 75, 77, 75], label: "Crude oil prices" },
  ];
  lineChartLabels: Label[] = [
    "January",
    "February",
    "March",
    "April",
    "May",
    "June",
  ];
  lineChartOptions = {
    responsive: true,
  };
  lineChartColors: Color[] = [
    {
      borderColor: "black",
      backgroundColor: "rgba(255,255,0,0.28)",
    },
  ];

  lineChartLegend = true;
  lineChartPlugins = [];
  lineChartType = "line";
  ngOnInit() {}
  goToLoginPage() {
    this.router.navigate(["app/home"]);
  }
  goToRegisterPage() {}
  searchInstitution() {}
  registerInstitution() {
    this.router.navigate(["landingpage/registerinstitution"]);
  }
}
