import { NgModule, ErrorHandler, enableProdMode } from '@angular/core';
import { IonicApp, IonicModule, IonicErrorHandler } from 'ionic-angular';
import { MyApp } from './app.component';
import { EstimatePage } from '../pages/estimate/estimate';
import { CodePush } from '@ionic-native/code-push';
import { StatusBar } from '@ionic-native/status-bar';
import { SplashScreen } from '@ionic-native/splash-screen';

import '../utils/rxjs-extension';


import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';

// Import All Components
import { EstimateCardComponent } from '../components/estimate-card/estimate-card';
import { MapComponent } from '../components/map/map';
import { PincodeCardComponent } from '../components/pincode-card/pincode-card';
import { PropertyTypeCardComponent } from '../components/property-type-card/property-type-card';
import { PropertyVitalsComponent } from '../components/property-vitals/property-vitals';
import { ProgressCardComponent } from '../components/progress-card/progress-card';
import { CodePushComponent } from '../components/code-push/code-push';
enableProdMode();

@NgModule({
  declarations: [
    MyApp,
    EstimatePage,
    EstimateCardComponent,
    MapComponent,
    PincodeCardComponent,
    PropertyTypeCardComponent,
    PropertyVitalsComponent,
    ProgressCardComponent,
    CodePushComponent
  ],
  imports: [
    BrowserModule,
    HttpModule,
    IonicModule.forRoot(MyApp),
  ],
  bootstrap: [IonicApp],
  entryComponents: [
    MyApp,
    EstimatePage,
    EstimateCardComponent,
    MapComponent,
    PincodeCardComponent,
    PropertyTypeCardComponent,
    PropertyVitalsComponent,
    ProgressCardComponent,
    CodePushComponent
  ],
  providers: [{provide: ErrorHandler, useClass: IonicErrorHandler}, CodePush, StatusBar, SplashScreen, HttpModule]
})
export class AppModule {}
