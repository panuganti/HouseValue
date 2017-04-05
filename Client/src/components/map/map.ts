import { Component, ViewChild, ElementRef } from '@angular/core';

@Component({
  selector: 'map',
  templateUrl: 'map.html'
})
export class MapComponent {
  @ViewChild('map') mapElement: ElementRef;
  map: google.maps.Map;
  latLng: google.maps.LatLng;


  constructor() {
  }

  loadMapWithCoord(position, self) {
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
//    this.getLatLang();
  }

/*
    getLatLang() {
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
*/
}
