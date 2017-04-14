import { Component } from '@angular/core';
import { Platform } from 'ionic-angular';
import { StatusBar } from '@ionic-native/status-bar';
import { SplashScreen } from '@ionic-native/splash-screen';

import { EstimatePage } from '../pages/estimate/estimate';


@Component({
  templateUrl: 'app.html'
})
export class MyApp {
  rootPage: any = EstimatePage;

  constructor(public platform: Platform, public statusbar: StatusBar, public splashscreen: SplashScreen) {
    this.platform.ready().then(() => {
      this.statusbar.styleDefault();
      this.splashscreen.hide();
      this.rootPage = EstimatePage;
    });
  }

  ionViewDidLoad() {}
}
