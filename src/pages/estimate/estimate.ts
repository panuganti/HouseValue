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
  property: Property;
  propertyType: string;
  est: number = 0;
  address: string = '';
  bedrooms: number = 0;
  bathrooms: number = 0;
  plotSize: number = 0;
  builtUpSize: number = 0;
  status: string = 'ready';

  estimate() {
    var property  = {
        Bathrooms: 0,
        Bedrooms: 0,
        PlotSize: 0,
        BuiltUpArea: 0,
        YearConstructed: 1990,
        LatLng: {
          Lat: 0.0,
          Lng: 0.0
        },
    };
    // Check if required properties are there
    this.property = property;
    this.property.Bathrooms = this.bathrooms;
    this.property.Bedrooms = this.bedrooms;
    this.property.PlotSize = this.plotSize;
    this.property.BuiltUpArea = this.builtUpSize;
    this.property.YearConstructed = 1990;
    this.service.getEstimate(this.property).toPromise().then(res => this.est = res);
  }

  continueForAccuracy() {
    this.estimate();
    this.showAreaCard = false;
    this.showEstimate = true;
    this.showAdditionalDetails = true;
  }

  constructor(public navCtrl: NavController, public navParams: NavParams, public service: ServiceCaller) {
  }

  onCancel(event) {
  }

  onInput(event) {
    if (this.address.length == 6) {
      this.loadMap();
    }
  }

  getCurrentLocation() {
    if (navigator.geolocation) {
      let self = this;
      navigator.geolocation.getCurrentPosition(function (position) {
        self.loadMapWithCoord(position, self);
      });
    }
    else {
      console.log('not supported');
    }
  }

  loadMapWithCoord(position, self) {
    this.hideMap = false;
    let latLng = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
    let mapOptions = { center: latLng, zoom: 15, mapTypeId: google.maps.MapTypeId.ROADMAP };
    self.map = new google.maps.Map(self.mapElement.nativeElement, mapOptions);
    var marker = new google.maps.Marker({
      map: this.map,
      position: latLng,
      draggable: true,
      animation: google.maps.Animation.DROP,
      icon: { path: google.maps.SymbolPath.BACKWARD_CLOSED_ARROW, scale: 6 },
    });
    marker.addListener('dragend', function () {
      self.latLng = marker.getPosition();
      console.log(self.latLng);
    });
    self.showContinueFromMap = true;
    self.latLng = latLng;
  }

  loadMap() {
    let latLng = new google.maps.LatLng(-34.9290, 138.6010);
    let mapOptions = { center: latLng, zoom: 15, mapTypeId: google.maps.MapTypeId.ROADMAP };
    this.map = new google.maps.Map(this.mapElement.nativeElement, mapOptions);
    this.getLatLang();
    this.hideMap = false;
    this.showContinueFromMap = true;
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

  incBedrooms() {
    this.bedrooms++;
  }

  decBedrooms() {
    if (this.bedrooms > 0) {
      this.bedrooms--;
    }
  }

  incBathrooms() {
    this.bathrooms++;
  }

  decBathrooms() {
    if (this.bathrooms > 0) {
      this.bathrooms--;
    }
  }


  propertySelection(event, item) {
  }

  getLatLang() {
    this.hideMap = false;
    var latLngResponse = this.service.getLatLong(this.address);
    latLngResponse.toPromise().then(res => {
      this.map.setCenter(res.results[0].geometry.location);
      var marker = new google.maps.Marker({
        map: this.map,
        position: res.results[0].geometry.location,
        draggable: true,
        animation: google.maps.Animation.DROP,
        icon: { path: google.maps.SymbolPath.BACKWARD_CLOSED_ARROW, scale: 6 },
      });
      marker.addListener('dragend', function () {
        this.latLng = marker.getPosition();
      });
      var infowindow = new google.maps.InfoWindow({ content: "Drag the marker to your house" });
      infowindow.open(this.map, marker);
    });
  }

  continueFromMap() {
    this.hideMap = true;
    this.showAddressCard = false;
    this.showPropertyTypeCard = true;
  }

  backToAddress() {
    this.showAddressCard = true;
    this.showPropertyTypeCard = false;
  }

  continueFromPropertyType() {
  }

  continueFromResidentialPropertyType() {
    this.showAreaCard = true;
    this.showPropertyTypeCard = false;
  }

  backToProperty() {
    this.showPropertyTypeCard = true;
  }

  backToPropertyType() {
    this.showPropertyTypeCard = true;
  }

  hideMap: boolean = true;
  showEstimate: boolean = true;
  showPropertyTypeCard: boolean = false;
  showContinueFromMap: boolean = false;
  showAddressCard: boolean = true;
  showCommercialPropertyType: boolean = false;
  showAgriculturalPropertyType: boolean = false;
  showAreaCard: boolean = false;
  showAdditionalDetails: boolean = false;

  city: string;
}
