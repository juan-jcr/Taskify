import {Component, inject, signal, ViewChild} from '@angular/core';
import { TaskListComponent } from '../../components/task-list/task-list.component';
import {TaskService} from '../../service/task.service';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';
import {TaskRequest} from '../../interface/TaskRequest';


@Component({
  selector: 'app-tasks-page',
  imports: [TaskListComponent, FormsModule, ReactiveFormsModule],
  templateUrl: './tasks-page.component.html',
})
export default class TasksPageComponent {
  @ViewChild(TaskListComponent) taskListComponent!: TaskListComponent;
  private readonly taskService = inject(TaskService);
  private readonly taskBuilder = inject(FormBuilder);

  taskForm: FormGroup = this.taskBuilder.group({
    title: ['', Validators.required],
    description: [''],
    dateOfCreation: [null]
  });
  errorMessage = signal<string>('');

  submit() {
    if (this.taskForm.invalid) {
      return;
    }
    this.errorMessage.set('');

    const taskRequest: TaskRequest = this.taskForm.value;

    this.taskService.addTask(taskRequest).subscribe({
      next: () => {
        this.taskListComponent.loadTasks();
        this.taskForm.reset();
      },
      error: () => {
        this.errorMessage.set("error");
      }
    })
  }
}
