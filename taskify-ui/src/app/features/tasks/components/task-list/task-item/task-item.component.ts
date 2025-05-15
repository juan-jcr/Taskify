import {Component, EventEmitter, Input, Output} from '@angular/core';
import {TaskRequest} from '../../../interface/TaskRequest';
import {DatePipe, NgClass} from '@angular/common';

@Component({
  selector: 'app-task-item',
  imports: [
    DatePipe,
  ],
  templateUrl: './task-item.component.html',
})
export class TaskItemComponent {
  @Input({required:true}) task!: TaskRequest;
  @Output() toggleCompleted = new EventEmitter<TaskRequest>();

  onToggleCompleted(): void {
    this.toggleCompleted.emit(this.task);
  }
}
