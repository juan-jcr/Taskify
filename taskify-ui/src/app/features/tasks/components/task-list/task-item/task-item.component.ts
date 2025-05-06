import {Component, Input} from '@angular/core';
import {TaskRequest} from '../../../interface/TaskRequest';

@Component({
  selector: 'app-task-item',
  imports: [],
  templateUrl: './task-item.component.html',
})
export class TaskItemComponent {
  @Input({required:true}) task!: TaskRequest

}
