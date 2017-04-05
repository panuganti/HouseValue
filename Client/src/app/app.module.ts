import { NgModule, ErrorHandler, enableProdMode } from '@angular/core';
import { IonicApp, IonicModule, IonicErrorHandler } from 'ionic-angular';
import { MyApp } from './app.component';
import { Estimate } from '../pages/estimate/estimate';
import '../utils/rxjs-extension';

// Import All Components
import {EstimateCardComponent} from '../components/estimate-card/estimate-card';
import { MapComponent } from '../components/map/map';
import { PincodeCardComponent } from '../components/pincode-card/pincode-card';
import { PropertyTypeCardComponent } from '../components/property-type-card/property-type-card';
import { PropertyVitalsComponent } from '../components/property-vitals/property-vitals';
enableProdMode();

@NgModule({
  declarations: [
    MyApp,
    Estimate,
    EstimateCardComponent,
    MapComponent,
    PincodeCardComponent,
    PropertyTypeCardComponent,
    PropertyVitalsComponent
  ],
  imports: [
    IonicModule.forRoot(MyApp),
  ],
  bootstrap: [IonicApp],
  entryComponents: [
    MyApp,
    Estimate,
    EstimateCardComponent,
    MapComponent,
    PincodeCardComponent,
    PropertyTypeCardComponent,
    PropertyVitalsComponent
  ],
  providers: [{provide: ErrorHandler, useClass: IonicErrorHandler}]
})
export class AppModule {}
