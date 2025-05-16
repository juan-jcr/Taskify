import {Component, ElementRef, EventEmitter, HostListener, Input, Output} from '@angular/core';
import {TaskRequest} from '../../../interface/TaskRequest';
import {DatePipe} from '@angular/common';

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
  @Output() deleteTask = new EventEmitter<number>();

  isOpen = false;
  constructor(private eRef: ElementRef) {}

  toggleDropdown() {
    this.isOpen = !this.isOpen;
  }

  closeDropdown() {
    this.isOpen = false;
  }
  @HostListener('document:click', ['$event'])
  handleClickOutside(event: MouseEvent) {
    if (!this.eRef.nativeElement.contains(event.target)) {
      this.isOpen = false;
    }
  }

  onToggleCompleted(): void {
    this.toggleCompleted.emit(this.task);
  }
  onDeleteTask(): void {
    this.deleteTask.emit(this.task.id);
  }

}
