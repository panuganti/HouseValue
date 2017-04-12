import { NavController, NavParams } from 'ionic-angular';

import { Component, NgZone } from '@angular/core';
import { Platform, Events, AlertController } from 'ionic-angular';

import { CodePush, SyncStatus } from '@ionic-native/code-push';

@Component({
  selector: 'page-code-push',
  templateUrl: 'code-push.html'
})
export class CodePushPage {

  status: string;
  downloadProgress = (progress) => { console.log(`Downloaded ${progress.receivedBytes} of ${progress.totalBytes}`); }

  constructor(public alertController: AlertController, public ngZone: NgZone, public platform: Platform,
    public events: Events, public navCtrl: NavController, public navParams: NavParams, public codepush: CodePush) {

    this.platform.ready().then(() => {
      this.codepush.sync().subscribe((syncStatus) => {
        if (syncStatus == SyncStatus.UP_TO_DATE) {
          // facing some zoning problems here !!
          // forcing to run in the ngzone
          this.ngZone.run(() => {
            this.status = "Up to date";
            // TODO: Navigate to home
          });
        }

        // not facing zoning issue here ?
        switch (syncStatus) {
          case SyncStatus.IN_PROGRESS:
            this.status = 'in progress'; break;
          case SyncStatus.CHECKING_FOR_UPDATE:
            this.status = 'checking for update'; break;
          case SyncStatus.DOWNLOADING_PACKAGE:
            this.status = 'Downloading package'; break;
          case SyncStatus.INSTALLING_UPDATE:
            this.status = 'Installing Update'; break;
          case SyncStatus.UPDATE_INSTALLED:
            this.status = 'Update installed. Will be available upon restart';
            break;
          case SyncStatus.ERROR:
            this.status = 'Error'; break;
          default:
            this.status = 'This should never happen'; break;
        }
      }, this.downloadProgress); // codepush
    }); // platform ready
  }

  ionViewDidLoad() {
  }

}
