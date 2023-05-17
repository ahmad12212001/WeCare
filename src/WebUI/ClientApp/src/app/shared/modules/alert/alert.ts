export class Alert {
  id!: string;
  header!: string;
  body!: string;
  image!: string;
  duration!: number;
  autoClose!: boolean;
  type!: NotificationType;
  actionMessage!: string;
  cssClass!: string;
  showCloseButton!: boolean;
  closeButtonText!: string;
  closeButtonCss: string = 'btn-white';
  label!: string;
  placeholder!: string;
  errorMessage!: string;
  constructor(init: Partial<Alert>) {
    Object.assign(this, init);
  }
}
export enum NotificationType {
  Success,
  Error,
  Info,
  Warning,
  Note,
}