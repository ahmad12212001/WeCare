import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { FormBase } from '@shared/models/form-base';

@Component({
  selector: 'feed-back',
  templateUrl: './feed-back.component.html',
  styleUrls: ['./feed-back.component.scss']
})
export class FeedBackComponent extends FormBase implements OnInit {
  @Input() studentName: string;
  @Input() requestId: number;
  feedbackFrom: FormGroup;

  constructor(private _fb: FormBuilder) {
    super();
  }

  ngOnInit() {
    this.createFeedBackForm();
  }

  createFeedBackForm() {
    this.feedbackFrom = this._fb.group({
      requestId: new FormControl(this.requestId, Validators.required),
      comment: new FormControl('', Validators.required),
      rate: new FormControl(null, Validators.required)
    })
  }
  onSubmit() {
    if (this.feedbackFrom.invalid) {
      this.onSubmit$.next(null);
      setTimeout(() => {
        this.onSubmit$.next(true);
      }, 10);
      return;
    }

    this.closeDialog$.next(this.feedbackFrom.value);

  }

  close() {
    this.closeDialog$.next(null);
  }
}
