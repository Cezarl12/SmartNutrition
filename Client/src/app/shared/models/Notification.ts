export type NotificationType = 'success' | 'error';

export type NotificationSnackBar = {
    type: NotificationType;
    title: string;
    message: string;
}