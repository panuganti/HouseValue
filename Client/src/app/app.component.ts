import { Component, ViewChild } from '@angular/core';
import { Nav, Platform } from 'ionic-angular';
import { StatusBar, Splashscreen } from 'ionic-native';

import { EstimatePage } from '../pages/estimate/estimate';


@Component({
  templateUrl: 'app.html'
})
export class MyApp {
  @ViewChild(Nav) nav: Nav;

  rootPage: any = EstimatePage;

  pages: Array<{ title: string, component: any }>;

  constructor(public platform: Platform) {
    this.platform.ready().then(() => {
      StatusBar.styleDefault();
      Splashscreen.hide();
      this.rootPage = EstimatePage;
    });
  }

  ionViewDidLoad() {}
}
