import {Component, Input} from '@angular/core';
import {TaskRequest} from '../../../interface/TaskRequest';
import {DatePipe} from '@angular/common';

@Component({
  selector: 'app-task-item',
  imports: [
    DatePipe
  ],
  templateUrl: './task-item.component.html',
})
export class TaskItemComponent {
  @Input({required:true}) task!: TaskRequest
}
