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
        this.tasks.set(tasks);
        this.loading.set(false);
      },
      error: error => {
        this.error.set("Error al cargar las tareas");
        this.loading.set(false);
      }
    })
  }

  onToggleCompleted(task: TaskRequest): void {
    const updatedTask = { ...task, completed: !task.completed };

    this.tasks.update(tasks =>
      tasks.map(t => t.id === task.id ? updatedTask : t)
    );

    this.taskService.updateTask(updatedTask).subscribe({
      error:() => {
        this.error.set("Error al actualizar las tareas");
        this.tasks.update(tasks =>
          tasks.map(t => t.id === task.id ? task : t)
        );
      }
    });
  }

  onDeleteTask(taskId: number){
    this.taskService.deleteTask(taskId).subscribe({
      next: () => {
        this.tasks.update(tasks => tasks.filter(t => t.id !== taskId));
      },
      error: () => {
        this.error.set('Error al eliminar la tarea')
      }
    })
  }
}
