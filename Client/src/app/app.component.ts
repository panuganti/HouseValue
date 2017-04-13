import { Component } from '@angular/core';
import { Platform } from 'ionic-angular';
import { StatusBar, Splashscreen } from 'ionic-native';

import { EstimatePage } from '../pages/estimate/estimate';


@Component({
  templateUrl: 'app.html'
})
export class MyApp {
  rootPage: any = EstimatePage;

  constructor(public platform: Platform) {
    this.platform.ready().then(() => {
      StatusBar.styleDefault();
      Splashscreen.hide();
      this.rootPage = EstimatePage;
    });
  }

  ionViewDidLoad() {}
}
