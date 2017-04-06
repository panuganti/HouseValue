import { Input, Component } from '@angular/core';

@Component({
  selector: 'progress-card',
  templateUrl: 'progress-card.html'
})
export class ProgressCardComponent {
  @Input() step: number;
  constructor() {
  }

}
