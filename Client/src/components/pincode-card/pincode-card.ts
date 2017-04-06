import { Input, Component, Output, EventEmitter } from '@angular/core';


@Component({
  selector: 'pincode-card',
  templateUrl: 'pincode-card.html'
})
export class PincodeCardComponent {
  @Output() location: EventEmitter<any> = new EventEmitter<any>();

  isGeoSupported: boolean = true;
  coords: any;
  @Input() pincode: string;
  constructor() {
  }

  onInput(event) {
    if (this.pincode.length == 6) {
      this.location.emit({ lat: "", lng: "", pincode: this.pincode, type: "pincode" });
    }
    // TODO: hide continue button if changed.
  }

  useGeo() {
    this.location.emit({ lat: this.coords.lat, lng: this.coords.lng, pincode: "", type: "latlng" });
  }

  ngOnInit() {
    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition((position) => {
        this.coords = { lat: position.coords.latitude, lng: position.coords.longitude };
      }, 
      (error) => {this.isGeoSupported = false; });
    }
    else {
      // will never hit
      this.isGeoSupported = false;
    }
  }

  onCancel(event) {
    this.pincode = '';
  }

}
