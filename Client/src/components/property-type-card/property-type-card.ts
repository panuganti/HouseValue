import { Output, EventEmitter, Component } from '@angular/core';

@Component({
  selector: 'property-type-card',
  templateUrl: 'property-type-card.html'
})
export class PropertyTypeCardComponent {
  type: string;
  @Output() propertyType: EventEmitter<string> = new EventEmitter<string>();

  constructor() {
  }

  select() {
    this.propertyType.emit(this.type);
  }

}
