import {
  Injectable,
  ViewChild,
  ElementRef,
  Input,
  OnInit,
  Component,
} from "@angular/core";
import { environment } from "../../environments/environment";
declare var H: any;

@Component({
  selector: "here-map",
  templateUrl: "./here-map.component.html",
  styleUrls: ["./here-map.component.css"],
})
@Injectable({
  providedIn: "root",
})
export class HereService implements OnInit {
  @ViewChild("map")
  public mapElement: ElementRef;

  @Input()
  public lat: any;

  @Input()
  public lng: any;

  @Input()
  public width: any;

  @Input()
  public height: any;

  private platform: any;
  private map: any;
  public geocoder: any;
  private ui: any;
  private search: any;
  public constructor() {}
  ngOnInit() {
    this.platform = new H.service.Platform({
      apikey: "YUexL3Lkc0MEBXdCuQiW1XRYCyNcpulUtjiL-JdOeqI",
    });
    this.geocoder = this.platform.getGeocodingService();
    this.search = new H.places.Search(this.platform.getPlacesService());
  }
  public getAddress(query: string) {
    return new Promise((resolve, reject) => {
      this.geocoder.geocode(
        { searchText: query },
        (result) => {
          if (result.Response.View.length > 0) {
            if (result.Response.View[0].Result.length > 0) {
              resolve(result.Response.View[0].Result);
            } else {
              reject({ message: "no results found" });
            }
          } else {
            reject({ message: "no results found" });
          }
        },
        (error) => {
          reject(error);
        }
      );
    });
  }
  public ngAfterViewInit() {
    let defaultLayers = this.platform.createDefaultLayers();
    this.map = new H.Map(
        this.mapElement.nativeElement,
        defaultLayers.normal.map,
        {
            zoom: 10,
            center: { lat: this.lat, lng: this.lng }
        }
    );
    let behavior = new H.mapevents.Behavior(new H.mapevents.MapEvents(this.map));
    this.ui = H.ui.UI.createDefault(this.map, defaultLayers);
}

  public getAddressFromLatLng(query: string) {
    return new Promise((resolve, reject) => {
      this.geocoder.reverseGeocode(
        { prox: query, mode: "retrieveAddress" },
        (result) => {
          if (result.Response.View.length > 0) {
            if (result.Response.View[0].Result.length > 0) {
              resolve(result.Response.View[0].Result);
            } else {
              reject({ message: "no results found" });
            }
          } else {
            reject({ message: "no results found" });
          }
        },
        (error) => {
          reject(error);
        }
      );
    });
  }
  public places(query: string) {
    this.map.removeObjects(this.map.getObjects());
    this.search.request({ "q": query, "at": this.lat + "," + this.lng }, {}, data => {
        for(let i = 0; i < data.results.items.length; i++) {
            this.dropMarker({ "lat": data.results.items[i].position[0], "lng": data.results.items[i].position[1] }, data.results.items[i]);
        }
    }, error => {
        console.error(error);
    });
}

private dropMarker(coordinates: any, data: any) {
    let marker = new H.map.Marker(coordinates);
    marker.setData("<p>" + data.title + "<br>" + data.vicinity + "</p>");
    marker.addEventListener('tap', event => {
        let bubble =  new H.ui.InfoBubble(event.target.getPosition(), {
            content: event.target.getData()
        });
        this.ui.addBubble(bubble);
    }, false);
    this.map.addObject(marker);
}
}
