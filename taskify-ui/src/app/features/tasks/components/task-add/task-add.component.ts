import {Component, EventEmitter, inject, Input, Output, signal} from '@angular/core';
import {TaskService} from '../../service/task.service';
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from '@angular/forms';
import {TaskRequest} from '../../interface/TaskRequest';

@Component({
  selector: 'app-task-add',
  imports: [FormsModule, ReactiveFormsModule],
  templateUrl: './task-add.component.html',
})
export class TaskAddComponent {
  private readonly taskService = inject(TaskService);
  private readonly taskBuilder = inject(FormBuilder);
  @Output() loadTask = new EventEmitter<void>();
  errorMessage = signal<null | string>(null);


  taskForm: FormGroup = this.taskBuilder.group({
    title: ['', Validators.required],
    description: [''],
    dateOfCreation: [null]
  });

  submit() {
    if (this.taskForm.invalid) {
      return;
    }
    this.errorMessage.set('');

    const taskRequest: TaskRequest = this.taskForm.value;

    this.taskService.addTask(taskRequest).subscribe({
      next: () => {
        this.loadTask.emit();
        this.taskForm.reset();
      },
      error: () => {
        this.errorMessage.set('error occurred');
      }
    })
  }



}
