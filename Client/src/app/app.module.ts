import { NgModule, ErrorHandler } from '@angular/core';
import { IonicApp, IonicModule, IonicErrorHandler } from 'ionic-angular';
import { MyApp } from './app.component';
import { Welcome } from '../pages/welcome/welcome';
import { Estimate } from '../pages/estimate/estimate';
import '../utils/rxjs-extension';



@NgModule({
  declarations: [
    MyApp,
    Welcome,
    Estimate,
  ],
  imports: [
    IonicModule.forRoot(MyApp),
  ],
  bootstrap: [IonicApp],
  entryComponents: [
    MyApp,
    Welcome,
    Estimate,
  ],
  providers: [{provide: ErrorHandler, useClass: IonicErrorHandler}]
})
export class AppModule {}
