import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Exam } from '@app/pages/models/exam';
import { CoursesService } from '@app/pages/services/courses.service';
import { ExamService } from '@app/pages/services/exam.service';
import { FormBase } from '@shared/models/form-base'
import { Option } from '@shared/models/option';
import { map, takeUntil } from 'rxjs';
import * as moment from 'moment-timezone';
@Component({
  selector: 'exam-operation',
  templateUrl: './exam-operation.component.html',
  styleUrls: ['./exam-operation.component.scss']
})
export class ExamOperationComponent extends FormBase implements OnInit {

  examForm: FormGroup;
  courses: Option[] = [];

  constructor(private _fb: FormBuilder,
    private _examService: ExamService,
    private _courseService: CoursesService,
    private route: ActivatedRoute) {
    super();
  }

  ngOnInit() {
    this.createExamForm();

    this._courseService.getAcacdemicStaffCourses().pipe(map(i => {
      return i.map(c => {
        return {
          id: c.id,
          name: c.name
        }
      })
    })).subscribe(res => {
      this.courses = res;
      const id = this.route.snapshot.params.id;
      if (id && id > 0) {
        this._examService.getExam(id).pipe(takeUntil(this.onDestroy$)).subscribe(res => {
          this.examForm.setValue({
            id: res.id,
            hallNo: res.hallNo,
            dueDate: moment(res.dueDate).format(
              'YYYY-MM-DD'
            ),
            location: res.location,
            courseId: this.courses.find(i => i.id == res.courseId)
          })
        })
      }
    });


  }

  createExamForm() {
    this.examForm = this._fb.group({
      hallNo: new FormControl(null, Validators.required),
      dueDate: new FormControl(null, Validators.required),
      location: new FormControl(''),
      courseId: new FormControl(null, Validators.required),
      id: new FormControl(null)
    })
  }

  onSubmit() {
    if (this.examForm.invalid) {
      this.onSubmit$.next(null);
      setTimeout(() => {
        this.onSubmit$.next(true);
      }, 10);
      return;
    }

    let exam: Exam = {
      dueDate: this.examForm.value.dueDate,
      courseId: this.examForm.value.courseId.id,
      hallNo: this.examForm.value.hallNo,
      id: this.examForm.value.id,
      location: this.examForm.value.location
    }

    if (exam.id) {
      this._examService.updateExam(exam).pipe(takeUntil(this.onDestroy$)).subscribe(res => {
        debugger
      })
    } else {
      this._examService.createExam(exam).pipe(takeUntil(this.onDestroy$)).subscribe(res => {
        debugger
      })
    }

  }

}
