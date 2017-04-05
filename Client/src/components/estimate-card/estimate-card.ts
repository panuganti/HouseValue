import { Input, Component } from '@angular/core';
@Component({
  selector: 'estimate-card',
  templateUrl: 'estimate-card.html'
})
export class EstimateCardComponent {
@Input() estimate: string;
pValue: number = 1;

  constructor() {
  }

  format(estimate: number): string {
      return estimate.toLocaleString('en-IN', { maximumFractionDigits: 0, style: 'currency', currency: 'INR' });
  }

}
