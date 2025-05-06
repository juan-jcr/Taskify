import {Component, inject, signal} from '@angular/core';
import {TaskService} from '../../service/task.service';
import {TaskRequest} from '../../interface/TaskRequest';
import {TaskItemComponent} from './task-item/task-item.component';

@Component({
  selector: 'app-task-list',
  imports: [
    TaskItemComponent
  ],
  templateUrl: './task-list.component.html',
})
export class TaskListComponent {
  private taskService = inject(TaskService);

  tasks = signal<TaskRequest[]>([]);
  loading = signal(false);
  error = signal<null | string>(null);
  constructor(){
    this.loadTasks();
  }
  loadTasks(){
    this.loading.set(true);
    this.error.set(null);

    this.taskService.getTasks().subscribe({
      next: tasks => {
        this.tasks = signal(tasks);
        this.loading = signal(false);
      },
      error: error => {
        this.error.set("Error al cargar las tareas");
      }
    })
  }
}
