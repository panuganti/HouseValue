import { Component, ViewChild } from '@angular/core';
import { Nav, Platform } from 'ionic-angular';
import { StatusBar, Splashscreen } from 'ionic-native';

import { CodePushPage } from '../pages/code-push/code-push';


@Component({
  templateUrl: 'app.html'
})
export class MyApp {
  @ViewChild(Nav) nav: Nav;

  rootPage: any = CodePushPage;

  pages: Array<{ title: string, component: any }>;

  constructor(public platform: Platform) {
    this.platform.ready().then(() => {
      StatusBar.styleDefault();
      Splashscreen.hide();
      this.nav.setRoot(CodePushPage);
    });
  }
}
