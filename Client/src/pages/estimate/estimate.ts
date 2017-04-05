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

  property: Property;
  propertyType: string;
  est: number = 0;
  address: string = '';
  bedrooms: number = 0;
  bathrooms: number = 0;
  plotSize: number = 0;
  builtUpSize: number = 0;
  status: string = 'ready';

  constructor(public navCtrl: NavController, public navParams: NavParams, public service: ServiceCaller) {
  }

  ionViewDidLoad() {
    // Set some defaults
    this.property = {
      LatLng: {
        Lat: 0.0,
        Lng: 0.0
      },
      YearConstructed: 1990,
      BuiltUpArea: 0,
      PlotSize: 0,
      Bedrooms: 0,
      Bathrooms: 0,
      Pincode: '500027',
      UnderConstruction: false,
      FloorCount: 3,
      FloorNumber: 1
    };
    let latLng = new google.maps.LatLng(-34.9290, 138.6010);
    let mapOptions = { center: latLng, zoom: 15, mapTypeId: google.maps.MapTypeId.ROADMAP };
    this.map = new google.maps.Map(this.mapElement.nativeElement, mapOptions);
  }

  prev() { 
    if (this.show_map){
      this.show_map = false;
      this.show_pincode_card = true;
    }
    if (this.show_type_card) {
      this.show_type_card = false;
      this.show_map = true;
    }
  }

  next() {
    if (this.show_pincode_card) {
      this.loadMapWithPincode(this.property.Pincode);
    }
    if (this.show_map) {
      this.show_map = false;
      this.show_type_card = true;
    }
    if (this.show_type_card) {
      this.show_vitals_card = true;
    }
    if (this.show_vitals_card) {
      this.show_vitals_card = false;
      this.show_continue = false;
    }
  }

  @ViewChild('map') mapElement: ElementRef;
  map: google.maps.Map;
  latLng: google.maps.LatLng;
  show_pincode_card: boolean = true;
  show_map: boolean = false;
  show_continue: boolean = false;
  show_type_card: boolean = false;
  show_vitals_card: boolean = false;
  show_estimate:boolean = true;

  setLocation(ev) {
    if (ev.type == "pincode") {
      this.show_continue = true;
      this.property.Pincode = ev.pincode;
    }
    else if (ev.type == "latlng") {
      this.latLng = new google.maps.LatLng(ev.lat, ev.lng);
      this.loadMapWithCoord(this.latLng);
    }
  }

  loadMapWithPincode(pincode: string) {
    var latLngResponse = this.service.getLatLong(pincode);
    latLngResponse.toPromise().then((res) => {this.loadMapWithCoord(res.results[0].geometry.location);});
  }

  loadMapWithCoord(latlng: google.maps.LatLng) {
    this.show_pincode_card = false;
    this.show_map = true;
    let mapOptions = { center: this.latLng, zoom: 15, mapTypeId: google.maps.MapTypeId.ROADMAP };
    console.log(this.show_map);
    this.map = new google.maps.Map(this.mapElement.nativeElement, mapOptions);
    var marker = new google.maps.Marker({
      map: this.map,
      position: this.latLng,
      draggable: true,
      animation: google.maps.Animation.DROP,
      icon: { path: google.maps.SymbolPath.BACKWARD_CLOSED_ARROW, scale: 6 },
    });
    marker.addListener('dragend', function () {
      this.latLng = marker.getPosition();
    });
    var infowindow = new google.maps.InfoWindow({ content: "Drag the marker to your house" });
    infowindow.open(this.map, marker);
    // TODO: Get Pincode from latlng
  }

  continue: string = 'Continue ';
  back: string = 'Back';
  showEstimate: boolean = true;
  showPropertyTypeCard: boolean = false;
  showContinueFromMap: boolean = false;
  showAddressCard: boolean = true;
  showCommercialPropertyType: boolean = false;
  showAgriculturalPropertyType: boolean = false;
  showAreaCard: boolean = false;
  showAdditionalDetails: boolean = false;



  estimate() {
    var property = {
      LatLng: {
        Lat: 0.0,
        Lng: 0.0
      },
      YearConstructed: 1990,
      BuiltUpArea: 0,
      PlotSize: 0,
      Bedrooms: 0,
      Bathrooms: 0,
      Pincode: '500027',
      UnderConstruction: false,
      FloorCount: 3,
      FloorNumber: 1
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


  getPropertyValue() {
    this.estimate();
    this.showAreaCard = false;
    this.showEstimate = true;
  }

  onCancel(event) {
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

  propertySelection(event, item) {
  }

  continueFromMap() {
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

  format(est: number): string {
    return est.toLocaleString('en-IN', { maximumFractionDigits: 0, style: 'currency', currency: 'INR' });
  }

  propertyTypeSelected(ev) {
    this.propertyType = ev;
  }

  updateVitals(ev) {
    this.property.PlotSize = ev.plotSize;
    this.property.BuiltUpArea = ev.builtUpSize;
    this.property.Bedrooms = ev.bedrooms;
    this.property.Bathrooms = ev.bathrooms;
    this.property.UnderConstruction = (ev.status == 'Under Construction');
    this.property.YearConstructed = ev.year;
  }

}
