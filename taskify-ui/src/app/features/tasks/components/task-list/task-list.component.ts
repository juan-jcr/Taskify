import {Component, EventEmitter, inject, Input, Output} from '@angular/core';
import {TaskRequest} from '../../interface/TaskRequest';
import {DatePipe, NgClass} from '@angular/common';
import {TaskService} from '../../service/task.service';



@Component({
  selector: 'app-task-list',
  imports: [DatePipe, NgClass],
  templateUrl: './task-list.component.html',
})
export class TaskListComponent {
  private readonly taskService = inject(TaskService);
  @Input() tasks: TaskRequest[] = [];
  @Input() loading: boolean = false;
  @Input() error: string | null = null;
  @Input() activeTask: TaskRequest | null = null;

  @Output() taskSelected = new EventEmitter<TaskRequest>();
  @Output() taskUpdated = new EventEmitter<TaskRequest>();
  @Output() deleteTask = new EventEmitter<number>();

  selectTask(task: TaskRequest) {
    this.taskSelected.emit(task);
  }

  isActive(task: TaskRequest): boolean {
    return this.activeTask?.id === task.id;
  }

  onToggleTask(task: TaskRequest) {
    const updatedTask = { ...task, completed: !task.completed };

    this.taskService.updateTask(updatedTask).subscribe({
      next: () => this.taskUpdated.emit(updatedTask),
      error: error => this.error = error,
    });
  }

  onDeleteTask(event: Event, taskId: number) {
    event.stopPropagation();
    this.taskService.deleteTask(taskId).subscribe({
      next: () => this.deleteTask.emit(taskId),
      error: error => this.error = error,
    });
  }
}
