import {fakeAsync, TestBed, tick} from '@angular/core/testing';
import { TaskUpdateComponent } from './task-update.component';
import { ReactiveFormsModule } from '@angular/forms';

import { of } from 'rxjs';
import {TaskService} from '../../service/task.service';
import {TaskRequest} from '../../interface/TaskRequest';

describe('TaskUpdateComponent', () => {
  let component: TaskUpdateComponent;
  let mockTaskService: jasmine.SpyObj<TaskService>;


  beforeEach(() => {
    mockTaskService = jasmine.createSpyObj('TaskService', ['updateTask']);

    TestBed.configureTestingModule({
      imports: [ReactiveFormsModule, TaskUpdateComponent],
      providers: [
        { provide: TaskService, useValue: mockTaskService }
      ]
    });

    component = TestBed.createComponent(TaskUpdateComponent).componentInstance;
  });

  it('debe crear', () => {
    expect(component).toBeTruthy();
  });

  describe('ngOnChanges', () => {
    it('debería actualizar los valores del formulario cuando cambie la entrada de la tarea', () => {
      const task: TaskRequest = {
        id: 1,
        title: 'Test Task',
        description: 'Test Desc',
        dateOfCreation: '2023-01-01T00:00:00',
        completed: true
      };

      component.task = task;
      component.ngOnChanges({
        task: {
          currentValue: task,
          previousValue: null,
          firstChange: false,
          isFirstChange: () => false
        }
      });

      expect(component.form.value).toEqual({
        title: 'Test Task',
        description: 'Test Desc',
        dateOfCreation: '2023-01-01',
        completed: true
      });
    });
  });

  describe('onSave', () => {
    it('no debe emitir ni llamar al servicio si el formulario es inválido', () => {
      component.task = { id: 1 } as TaskRequest;
      component.form.setErrors({ invalid: true });

      spyOn(component.taskUpdated, 'emit');

      component.onSave();

      expect(mockTaskService.updateTask).not.toHaveBeenCalled();
      expect(component.taskUpdated.emit).not.toHaveBeenCalled();
    });

    it('debería llamar al servicio y emitir un evento cuando el formulario sea válido', () => {
      const task: TaskRequest = {
        id: 1,
        title: 'Original Title',
        description: 'Original Desc',
        dateOfCreation: '2023-01-01',
        completed: false
      };

      component.task = task;

      component.form.patchValue({
        title: 'Updated Title',
        description: 'Updated Desc',
        dateOfCreation: '2023-01-01', // No olvidar este campo requerido
        completed: true
      });


      expect(component.form.valid).toBeTrue();

      const updatedTask = {
        ...task,
        ...component.form.value,
        dateOfCreation: '2023-01-01'
      };
      mockTaskService.updateTask.and.returnValue(of(updatedTask));

      spyOn(component.taskUpdated, 'emit');


      component.onSave();

      expect(mockTaskService.updateTask).toHaveBeenCalledWith(updatedTask);
      expect(component.taskUpdated.emit).toHaveBeenCalledWith(updatedTask);
      expect(component.message()).toBe('Guardado exitosamente');
    });


  });
});
