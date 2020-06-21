import { Injector, OnInit, Component, NgZone } from "@angular/core";
import { AppComponentBase } from "@shared/app-component-base";
import { Router } from "@angular/router";
<<<<<<< HEAD
import * as am4core from "@amcharts/amcharts4/core";
import * as am4charts from "@amcharts/amcharts4/charts";
import am4themes_animated from "@amcharts/amcharts4/themes/animated";

am4core.useTheme(am4themes_animated);

=======
>>>>>>> a585edc5d15350161a0705636c44a4ca688b1aac
@Component({
  templateUrl: "./initial-content.component.html",
})
export class InitialContentComponent extends AppComponentBase
  implements OnInit {
  private chart: am4charts.XYChart;
  constructor(private router: Router, injector: Injector) {
    super(injector);
  }
<<<<<<< HEAD
  ngOnInit() { }
  ngAfterViewInit() {

    let chart = am4core.create("chartdiv", am4charts.PieChart);

    // Add data
    chart.data = [{
      "plano": "Não possui plano",
      "value": 63.0
    }, {
      "plano": "Possui plano",
      "value": 37.0
    }
    ];

    // Add and configure Series
    let pieSeries = chart.series.push(new am4charts.PieSeries());
    pieSeries.dataFields.value = "value";
    pieSeries.dataFields.category = "plano";
    pieSeries.slices.template.stroke = am4core.color("#fff");
    pieSeries.slices.template.strokeOpacity = 1;

    // This creates initial animation
    pieSeries.hiddenState.properties.opacity = 1;
    pieSeries.hiddenState.properties.endAngle = -90;
    pieSeries.hiddenState.properties.startAngle = -90;

    chart.hiddenState.properties.radius = am4core.percent(0);
    let chart2 = am4core.create("chartdiv2", am4charts.XYChart);

    // Add data
    chart2.data = [{
      "horas": "< 1 hora>",
      "value": 26.0,
      "color": chart2.colors.next()
    }, {
      "horas": "1 ou 2 horas",
      "value": 45.0,
      "color": chart2.colors.next()
    }, {
      "horas": "3 ou 4 horas",
      "value": 24.0,
      "color": chart2.colors.next()
    }, {
      "horas": "> 5 horas",
      "color": chart2.colors.next(),
      "value": 5.0
    }];

    // Create axes
    let categoryAxis = chart2.xAxes.push(new am4charts.CategoryAxis());
    categoryAxis.dataFields.category = "horas";
    categoryAxis.renderer.grid.template.disabled = true;
    categoryAxis.renderer.minGridDistance = 30;
    categoryAxis.renderer.inside = true;
    categoryAxis.renderer.labels.template.fill = am4core.color("#fff");
    categoryAxis.renderer.labels.template.fontSize = 20;

    let valueAxis = chart2.yAxes.push(new am4charts.ValueAxis());
    valueAxis.renderer.grid.template.strokeDasharray = "4,4";
    valueAxis.renderer.labels.template.disabled = true;
    valueAxis.min = 0;


    // Do not crop bullets
    chart2.maskBullets = false;

    // Remove padding
    chart2.paddingBottom = 0;

    // Create series
    let series = chart2.series.push(new am4charts.ColumnSeries());
    series.dataFields.valueY = "value";
    series.dataFields.categoryX = "horas";
    series.columns.template.propertyFields.fill = "color";
    series.columns.template.propertyFields.stroke = "color";
    series.columns.template.column.cornerRadiusTopLeft = 15;
    series.columns.template.column.cornerRadiusTopRight = 15;
    series.columns.template.tooltipText = "{categoryX}: [bold]{valueY}[/b]";
    let chart3 = am4core.create("chartdiv3", am4charts.XYChart);

    // Add data
    chart3.data = [{
      "vezes": "0 a 4 vezes",
      "value": 79.0,
      "color": chart3.colors.next()
    }, {
      "vezes": "4 a 8 vezes",
      "value": 14.0,
      "color": chart3.colors.next()
    }, {
      "vezes": "8 a 10 vezes",
      "value": 4.0,
      "color": chart3.colors.next()
    }, {
      "vezes": "> 10 vezes",
      "color": chart3.colors.next(),
      "value": 3.0
    }];

    // Create axes
    let categoryAxis2 = chart3.xAxes.push(new am4charts.CategoryAxis());
    categoryAxis2.dataFields.category = "vezes";
    categoryAxis2.renderer.grid.template.disabled = true;
    categoryAxis2.renderer.minGridDistance = 30;
    categoryAxis2.renderer.inside = true;
    categoryAxis2.renderer.labels.template.fill = am4core.color("#fff");
    categoryAxis2.renderer.labels.template.fontSize = 20;

    let valueAxis2 = chart3.yAxes.push(new am4charts.ValueAxis());
    valueAxis2.renderer.grid.template.strokeDasharray = "4,4";
    valueAxis2.renderer.labels.template.disabled = true;
    valueAxis2.min = 0;

    // Do not crop bullets
    chart3.maskBullets = false;

    // Remove padding
    chart3.paddingBottom = 0;

    // Create series
    let series2 = chart3.series.push(new am4charts.ColumnSeries());
    series2.dataFields.valueY = "value";
    series2.dataFields.categoryX = "vezes";
    series2.columns.template.propertyFields.fill = "color";
    series2.columns.template.propertyFields.stroke = "color";
    series2.columns.template.column.cornerRadiusTopLeft = 15;
    series2.columns.template.column.cornerRadiusTopRight = 15;
    series2.columns.template.tooltipText = "{categoryX}: [bold]{valueY}[/b]";

    let chart4 = am4core.create("chartdiv4", am4charts.XYChart);
    chart4.hiddenState.properties.opacity = 0; // this creates initial fade-in

    chart4.maskBullets = false;

    let xAxis = chart4.xAxes.push(new am4charts.CategoryAxis());
    let yAxis = chart4.yAxes.push(new am4charts.CategoryAxis());

    xAxis.dataFields.category = "y";
    yAxis.dataFields.category = "x";

    xAxis.renderer.grid.template.disabled = true;
    xAxis.renderer.minGridDistance = 40;

    yAxis.renderer.grid.template.disabled = true;
    yAxis.renderer.inversed = true;
    yAxis.renderer.minGridDistance = 30;

    let series4 = chart4.series.push(new am4charts.ColumnSeries());
    series4.dataFields.categoryX = "y";
    series4.dataFields.categoryY = "x";
    series4.dataFields.value = "value";
    series4.sequencedInterpolation = true;
    series4.defaultState.transitionDuration = 3000;

    // Set up column appearance
    let column = series.columns.template;
    column.strokeWidth = 2;
    column.strokeOpacity = 1;
    column.stroke = am4core.color("#ffffff");
    column.tooltipText = "{x}, {y}: {value.workingValue.formatNumber('#.')}";
    column.width = am4core.percent(100);
    column.height = am4core.percent(100);
    column.column.cornerRadius(6, 6, 6, 6);
    column.propertyFields.fill = "color";

    // Set up bullet appearance
    let bullet1 = series.bullets.push(new am4charts.CircleBullet());
    //bullet1.circle.propertyFields.radius = "value";
    bullet1.circle.fill = am4core.color("#000");
    bullet1.circle.strokeWidth = 0;
    bullet1.circle.fillOpacity = 0.7;
    bullet1.interactionsEnabled = false;

    let bullet2 = series.bullets.push(new am4charts.LabelBullet());
    bullet2.label.text = "{value}";
    bullet2.label.fill = am4core.color("#fff");
    bullet2.zIndex = 1;
    bullet2.fontSize = 11;
    bullet2.interactionsEnabled = false;

    // define colors
    let colors = {
      "critical": "#ca0101",
      "bad": "#e17a2d",
      "medium": "#e1d92d",
      "good": "#5dbe24",
      "verygood": "#0b7d03"
    };

    chart4.data = [{
      "x": "Encontrar um centro de atendimento medico",
      "y": "Extremamente Fácil",
      "color": colors.medium,
      "value": 11
    }, {
      "x": "Marcar uma consulta medica",
      "y": "Extremamente Fácil",
      "color": colors.good,
      "value": 4
    }, {
      "x": "Encontrar um centro de atendimento medico",
      "y": "Fácil",
      "color": colors.bad,
      "value": 37
    }, {
      "x": "Marcar uma consulta medica",
      "y": "Fácil",
      "color": colors.medium,
      "value": 28
    }, {
      "x": "Encontrar um centro de atendimento medico",
      "y": "Moderadamente fácil",
      "color": colors.bad,
      "value": 31
    }, {
      "x": "Marcar uma consulta medica",
      "y": "Moderadamente fácil",
      "color": colors.bad,
      "value": 22
    }, {
      "x": "Encontrar um centro de atendimento medico",
      "y": "Pouco fácil",
      "color": colors.critical,
      "value": 11
    }, {
      "x": "Marcar uma consulta medica",
      "y": "Pouco fácil",
      "color": colors.critical,
      "value": 31
    }, {
      "x": "Encontrar um centro de atendimento medico",
      "y": "Nada fácil",
      "color": colors.critical,
      "value": 5
    }, {
      "x": "Marcar uma consulta medica",
      "y": "Nada fácil",
      "color": colors.critical,
      "value": 11
    }
    ];

    let baseWidth = Math.min(chart4.plotContainer.maxWidth, chart4.plotContainer.maxHeight);
    let maxRadius = baseWidth / Math.sqrt(chart4.data.length) / 2 - 2; // 2 is jast a margin
    series.heatRules.push({ min: 10, max: maxRadius, property: "radius", target: bullet1.circle });


    chart4.plotContainer.events.on("maxsizechanged", function () {
      let side = Math.min(chart4.plotContainer.maxWidth, chart4.plotContainer.maxHeight);
      bullet1.circle.clones.each(function (clone) {
        clone.scale = side / baseWidth;
      })
    })



  }

  ngOnDestroy() {
    if (this.chart) {
      this.chart.dispose();
    }
  }
=======
  ngOnInit() {}
>>>>>>> a585edc5d15350161a0705636c44a4ca688b1aac
  goToLoginPage() {
    this.router.navigate(["/app/home"]);
  }
  goToRegisterPage() { }
  searchInstitution() { }
  registerInstitution() {
    this.router.navigate(["landingpage/registerinstitution"]);
  }
}
