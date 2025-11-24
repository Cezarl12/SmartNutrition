import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { NotificationSnackBar } from '../../shared/models/Notification';

@Injectable({
  providedIn: 'root'
})
export class SnackBarService {

  private notificationSubject = new Subject<NotificationSnackBar>();

  public notification$ = this.notificationSubject.asObservable();


  error(message: string, title: string = 'Eroare') {
    this.notificationSubject.next({
      type: 'error',
      title: title,
      message: message
    });
  }

  success(message: string, title: string = 'Succes') {
    this.notificationSubject.next({
      type: 'success',
      title: title,
      message: message
    });
  }
}
