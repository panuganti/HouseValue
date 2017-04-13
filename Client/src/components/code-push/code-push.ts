import { Component, NgZone, Output, EventEmitter } from '@angular/core';
import { Platform } from 'ionic-angular';

import { CodePush, SyncStatus } from '@ionic-native/code-push';

@Component({
  selector: 'code-push',
  templateUrl: 'code-push.html'
})
export class CodePushComponent {
@Output() state: EventEmitter<string> = new EventEmitter<string>();

  constructor(public ngZone: NgZone, public platform: Platform, public codepush: CodePush) {
  }

  ngOnInit() {
    if (!this.platform.is('cordova')) { return;}
    this.platform.ready().then(() => {
      this.codepush.sync().subscribe((syncStatus) => {
        if (syncStatus == SyncStatus.UP_TO_DATE) {
          // facing some zoning problems here !!
          // forcing to run in the ngzone
          this.ngZone.run(() => {
            this.state.emit('Up to date');
          });
        }
        else {
          // not facing zoning issue here ?
          switch (syncStatus) {
            case SyncStatus.IN_PROGRESS:
              this.state.emit('in progress'); break;
            case SyncStatus.CHECKING_FOR_UPDATE:
              this.state.emit('checking for update'); break;
            case SyncStatus.DOWNLOADING_PACKAGE:
              this.state.emit('Downloading package'); break;
            case SyncStatus.INSTALLING_UPDATE:
              this.state.emit('Installing Update'); break;
            case SyncStatus.UPDATE_INSTALLED:
              this.state.emit('Update installed. Will be available upon restart');
              break;
            case SyncStatus.ERROR:
              this.state.emit('Error'); break;
            case SyncStatus.UPDATE_IGNORED:
              this.state.emit('Updage ignored'); break;
            default:
              this.state.emit(' '); break;
          }
        }
      });
    });
  }
}
