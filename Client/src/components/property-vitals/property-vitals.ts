import { Output, EventEmitter, Component } from '@angular/core';

@Component({
  selector: 'property-vitals',
  templateUrl: 'property-vitals.html'
})
export class PropertyVitalsComponent {
  plotSize: string;
  builtUpSize: string;
  bedrooms: number = 0;
  bathrooms: number = 0;
  status: string;
  year: string = '2017';

  @Output() vitals: EventEmitter<any> = new EventEmitter<any>();

  constructor() {
  }

  emit() {
    this.vitals.emit({
      plotSize: this.plotSize,
      builtUpSize: this.builtUpSize,
      bedrooms: this.bedrooms,
      bathrooms: this.bathrooms,
      status: this.status,
      year: this.year
    });
  }

  incBedrooms() {
    this.bedrooms++;
    this.emit();
  }

  decBedrooms() {
    if (this.bedrooms > 0) {
      this.bedrooms--;
      this.emit();
    }
  }

  incBathrooms() {
    this.bathrooms++;
    this.emit();
  }

  decBathrooms() {
    if (this.bathrooms > 0) {
      this.bathrooms--;
      this.emit();
    }
  }

}
