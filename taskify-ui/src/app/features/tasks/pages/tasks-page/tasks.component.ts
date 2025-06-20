import {Component, inject, OnInit, signal} from '@angular/core';
import { TaskListComponent } from '../../components/task-list/task-list.component';
import {TaskService} from '../../service/task.service';
import {TaskRequest} from '../../interface/TaskRequest';
import {TaskUpdateComponent} from '../../components/task-update/task-update.component';
import {AsideProfileComponent} from '../../components/aside-profile/aside-profile.component';
import {DatePipe} from '@angular/common';
import {TaskAddComponent} from '../../components/task-add/task-add.component';
import {AuthService} from '../../../auth/services/auth.service';
import {UserRequest} from '../../../auth/interface/user.interface';


@Component({
  selector: 'app-tasks-page',
  imports: [TaskListComponent, TaskUpdateComponent, AsideProfileComponent, DatePipe, TaskAddComponent],
  templateUrl: './tasks-page.component.html',
})
export default class TasksPageComponent implements OnInit{
  private readonly taskService = inject(TaskService);
  private readonly authService = inject(AuthService);

  tasks = signal<TaskRequest[]>([]);
  loading = signal(false);
  selectedTask = signal<TaskRequest | null>(null);
  userName: string = '';
  date = new Date();

  ngOnInit(){
    this.loadTasks()

    this.authService.getProfileName().subscribe({
      next: (data: UserRequest) => {
        this.userName = data.name;
      }
    })
  }

  loadTasks() {
    this.loading.set(true);

    this.taskService.getTasks().subscribe({
      next: tasks => {
        this.tasks.set(tasks);
        this.loading.set(false);
      }
    });
  }

  onTaskUpdated(updatedTask: TaskRequest) {
    this.tasks.update(tasks => tasks.map(t => t.id === updatedTask.id ? updatedTask : t));
  }

  onDeleteTask(taskId: number) {
    this.tasks.update(tasks => tasks.filter(t => t.id !== taskId));
    this.selectedTask.set(null);
  }

  onTaskSelected(task: TaskRequest) {
    this.selectedTask.set(task);
  }

  onCloseMenu() {
    this.selectedTask.set(null);
  }

}
