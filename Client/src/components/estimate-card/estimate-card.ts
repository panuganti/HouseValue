import { Input, Component } from '@angular/core';

@Component({
  selector: 'estimate-card',
  templateUrl: 'estimate-card.html'
})
export class EstimateCardComponent {
@Input() estimate: number;

  constructor() {
  }

  format(estimate: number): string {
      var lacs = estimate/100000;
      return lacs.toLocaleString('en-IN', { maximumFractionDigits: 2, style: 'currency', currency: 'INR' });
  }

}
