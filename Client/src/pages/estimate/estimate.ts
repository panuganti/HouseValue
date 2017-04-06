/// <reference path="../../../node_modules/@types/googlemaps/index.d.ts" />

import { Component, ViewChild, ElementRef } from '@angular/core';
import { NavController, NavParams } from 'ionic-angular';

import { ServiceCaller } from '../../providers/servicecaller';
//import { Observable } from 'rxjs/Observable';

import '../../utils/rxjs-extension';
import { Property } from '../../models/Property';

declare var google;

@Component({
  selector: 'page-estimate',
  providers: [ServiceCaller],
  templateUrl: 'estimate.html'
})
export class Estimate {
  @ViewChild('map') mapElement: ElementRef;
  map: google.maps.Map;
  latLng: google.maps.LatLng;
  show_pincode_card: boolean = true;
  show_map: boolean = false;
  show_continue: boolean = false;
  show_type_card: boolean = false;
  show_vitals_card: boolean = false;
  show_estimate: boolean = false;
  step: number = 1;
  pincode: string = '';
  nextDisabled: boolean = false;
  plot_size: number;
  builtup_size: number;
  construction_status: boolean;
  year:number;
  est: number = 0;
  continue: string = 'Continue ';
  back: string = 'Back';
  bedrooms: number = 0;
  bathrooms: number = 0;
  property_type: string;
  property: Property;

  constructor(public navCtrl: NavController, public navParams: NavParams, public service: ServiceCaller) {
  }

  ionViewDidLoad() {
    // Set some defaults
    this.property = this.getDefaultProperty();
    this.setDefaultMap();
  }

  setDefaultMap() {
    let latLng = new google.maps.LatLng(-34.9290, 138.6010);
    let mapOptions = { center: latLng, zoom: 15, mapTypeId: google.maps.MapTypeId.ROADMAP };
    this.map = new google.maps.Map(this.mapElement.nativeElement, mapOptions);
  }

  getDefaultProperty(): any {
    return {
      LatLng: { Lat: 0.0, Lng: 0.0},
      YearConstructed: 1990,
      BuiltUpArea: 0,
      PlotSize: 0,
      Bedrooms: 0,
      Bathrooms: 0,
      Pincode: '500036',
      UnderConstruction: false,
      FloorCount: 3,
      FloorNumber: 1
    };
  }

  restart() {
    this.step = 1;
    this.show_pincode_card = true;
    this.show_estimate = false;
    this.pincode = '';
  }

  prev() {
    this.nextDisabled = false;
    if (this.show_map) {
      this.show_map = false;
      this.show_pincode_card = true;
      this.step--;
    }
    else if (this.show_type_card) {
      this.show_type_card = false;
      this.show_map = true;
      this.step--;
    }
    else if (this.show_vitals_card) {
      this.show_vitals_card = false;
      this.show_type_card = true;
      this.step--;
    }
    else if (this.show_estimate) {
      this.show_estimate = false;
      this.show_vitals_card = true;
      this.step--;
    }
  }

  next() {
    if (this.show_pincode_card) {
      this.loadMapWithPincode(this.property.Pincode);
      this.step++;
    }
    if (this.show_map) {
      this.show_map = false;
      this.show_type_card = true;
      this.nextDisabled = true;
      this.step++;
    }
    else if (this.show_type_card) {
      this.show_type_card = false;
      this.show_vitals_card = true;
      this.nextDisabled = true;
      this.step++;
    }
    else if (this.show_vitals_card) {
      this.show_vitals_card = false;
      this.show_continue = false;
      this.nextDisabled = true;
      this.step++;
      this.show_estimate = true;
      this.estimate();
    }
  }

  propertyTypeSelected(ev) {
    this.property_type = ev;
    if (this.property_type != null) {
      this.nextDisabled = false;
    }
  }


  estimate() {
    this.property.Bathrooms = this.bathrooms;
    this.property.Bedrooms = this.bedrooms;
    this.property.PlotSize = this.plot_size;
    this.property.BuiltUpArea = this.builtup_size;
    this.property.YearConstructed = this.year;
    this.service.getEstimate(this.property).toPromise().then(res => {this.est = res;});
  }

  updateVitals(ev) {
    this.plot_size = ev.plotSize;
    this.builtup_size = ev.builtUpSize;
    this.bedrooms = ev.bedrooms;
    this.bathrooms = ev.bathrooms;
    this.construction_status = (ev.status == 'ready');
    this.year = ev.year;
    if (this.construction_status && this.plot_size > 0
         && this.builtup_size > 0 && this.bedrooms > 0 
         && this.bathrooms > 0) {
      this.nextDisabled = false;
    }
  }

  setLocation(ev) {
    if (ev.type == "pincode") {
      this.show_continue = true;
      this.pincode = ev.pincode;
    }
    else if (ev.type == "latlng") {
      this.latLng = new google.maps.LatLng(ev.lat, ev.lng);
      this.loadMapWithCoord(this.latLng, this);
    }
  }

  loadMapWithPincode(pincode: string) {
    var latLngResponse = this.service.getLatLong(pincode);
    latLngResponse.toPromise().then((res) => { this.loadMapWithCoord(res.results[0].geometry.location, this); });
  }

  loadMapWithCoord(latlng: google.maps.LatLng, self) {
    self.show_pincode_card = false;
    self.show_map = true;
    let mapOptions = { center: self.latLng, zoom: 15, mapTypeId: google.maps.MapTypeId.ROADMAP };
    console.log(this.mapElement.nativeElement);
    self.map = new google.maps.Map(self.mapElement.nativeElement, mapOptions);
    var marker = new google.maps.Marker({
      map: this.map,
      position: self.latLng,
      draggable: true,
      animation: google.maps.Animation.DROP,
      icon: { path: google.maps.SymbolPath.BACKWARD_CLOSED_ARROW, scale: 6 },
    });
    marker.addListener('dragend', function () {
      self.latLng = marker.getPosition();
    });
    var infowindow = new google.maps.InfoWindow({ content: "Drag the marker to your house" });
    infowindow.open(self.map, marker);
    // TODO: Get Pincode from latlng
  }

  /*
    p: (width: any) => Observable<string> = function (width) {
      return Observable.of("test");
    };
  
    caller: () => Promise<string> = async function () {
      let s: string = await this.p();
      let s2: string = await this.p();
      return "hello";
    }
  */
}
