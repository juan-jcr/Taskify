import { Component, EventEmitter, inject, Input, Output, SimpleChanges } from '@angular/core';
import { TaskService } from '../../service/task.service';
import { TaskRequest } from '../../interface/TaskRequest';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-task-update',
  imports: [ReactiveFormsModule],
  templateUrl: './task-update.component.html'
})
export class TaskUpdateComponent {
  @Input({required: true}) task: TaskRequest | null = null;
  @Output() closeMenu = new EventEmitter<void>();
  @Output () taskUpdated = new EventEmitter<TaskRequest>();

  private readonly taskService = inject(TaskService);
  private readonly fb = inject(FormBuilder);

  form: FormGroup = this.fb.group({
    title: ['', Validators.required],
    description: [''],
    dateOfCreation: [null],
    completed: [false]
  });

  ngOnChanges(changes: SimpleChanges) {
    if (this.task) {
      const formattedDate = this.task.dateOfCreation
      ? this.task.dateOfCreation.substring(0, 10)
      : null;
      this.form.patchValue({
        title: this.task.title,
        description: this.task.description,
        dateOfCreation: formattedDate,
        completed: this.task.completed

      });
    }
  }

  onSave() {
    if (this.form.invalid || !this.task) return;

    const updatedTask: TaskRequest = {
      ...this.task,
      ...this.form.value,
    };

    this.taskService.updateTask(updatedTask).subscribe({
      next: (task) => {
        this.taskUpdated.emit(task);

      },
      error: (err) => {
        console.error('Error updating task:', err);
      }
    });
  }



}
