import { Component, Output, EventEmitter } from '@angular/core';


@Component({
  selector: 'pincode-card',
  templateUrl: 'pincode-card.html'
})
export class PincodeCardComponent {
  @Output() location: EventEmitter<any> = new EventEmitter<any>();

  isGeoSupported: boolean = true;
  coords: any;
  pincode: string = '';
  constructor() {
  }

  onInput(event) {
    if (this.pincode.length == 6) {
      this.location.emit({ lat: "", lng: "", pincode: this.pincode, type: "pincode" });
    }
  }

  useGeo() {
    this.location.emit({ lat: this.coords.latitude, lng: this.coords.longitude, pincode: "", type: "latlng" });
  }

  ngOnInit() {
    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition((position) => {
        this.coords = { lat: position.coords.latitude, lng: position.coords.longitude };
      });
    }
    else {
      this.isGeoSupported = false;
    }
  }

}
