import { Component, ElementRef, EventEmitter, Input, Output } from '@angular/core';
import { TaskRequest } from '../../../interface/TaskRequest';
import { DatePipe, NgClass } from '@angular/common';

@Component({
  selector: 'app-task-item',
  imports: [
    DatePipe, NgClass
  ],
  templateUrl: './task-item.component.html',
})
export class TaskItemComponent {
  @Input({ required: true }) task!: TaskRequest;
  @Output() toggleCompleted = new EventEmitter<TaskRequest>();
  @Output() deleteTask = new EventEmitter<number>();
  @Output() taskSelected = new EventEmitter<TaskRequest>();


  onToggleCompleted(): void {
    this.toggleCompleted.emit(this.task);
  }

  onDeleteTask(): void {
    this.deleteTask.emit(this.task.id);
  }

  onSelectTask() {
    this.taskSelected.emit(this.task)
  }

}
