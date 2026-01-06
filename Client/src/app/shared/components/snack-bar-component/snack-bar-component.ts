import { NgClass } from '@angular/common';
import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { SnackBarService } from '../../../core/services/snackbar';
import { NotificationSnackBar } from '../../models/Notification';

export type NotificationType = 'success' | 'error'
@Component({
  selector: 'app-snack-bar-commponent',
  imports: [NgClass],
  templateUrl: './snack-bar-component.html',
  styleUrl: './snack-bar-component.css',
})


export class SnackBarComponent implements OnInit, OnDestroy {
  public isVisible = false;
  public type: NotificationType = 'error';
  public title = '';
  public message = '';

  private subscription?: Subscription;
  constructor(private snackBarService: SnackBarService, private cdr: ChangeDetectorRef) { }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
  ngOnInit(): void {
    this.subscription = this.snackBarService.notification$.subscribe(
      (notification: NotificationSnackBar) => {
        this.type = notification.type;
        this.title = notification.title;
        this.message = notification.message;
        this.isVisible = true;

        this.cdr.detectChanges();
        setTimeout(() => this.close(), 3000);
      }
    );
  }


  public close(): void {
    this.isVisible = false;
    this.cdr.detectChanges();
  }
}
