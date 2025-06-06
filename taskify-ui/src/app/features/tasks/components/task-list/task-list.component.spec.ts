
import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { TaskListComponent } from './task-list.component';

import { HttpClientTestingModule } from '@angular/common/http/testing';
import {EMPTY, of} from 'rxjs';
import {TaskService} from '../../service/task.service';
import {TaskRequest} from '../../interface/TaskRequest';

describe('TaskListComponent', () => {
  let component: TaskListComponent;
  let fixture: ComponentFixture<TaskListComponent>;
  let taskService: TaskService;

  const mockTasks: TaskRequest[] = [
    { id: 1, title: 'Task 1', description: 'Desc 1', dateOfCreation: '2023-01-01', completed: false },
    { id: 2, title: 'Task 2', description: 'Desc 2', dateOfCreation: '2023-01-02', completed: true }
  ];

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HttpClientTestingModule, TaskListComponent],
      providers: [TaskService]
    }).compileComponents();

    fixture = TestBed.createComponent(TaskListComponent);
    component = fixture.componentInstance;
    taskService = TestBed.inject(TaskService);
    fixture.detectChanges();
  });

  it('debe crear', () => {
    expect(component).toBeTruthy();
  });

  it('debería mostrar el estado de carga', () => {
    component.loading = true;
    fixture.detectChanges();

    const loadingElement = fixture.nativeElement.querySelector('p');
    expect(loadingElement).toBeTruthy();
    expect(loadingElement.textContent).toContain('Cargando tareas ...');
  });

  it('debería mostrar las tareas cuando se cargue', () => {
    component.tasks = mockTasks;
    component.loading = false;
    fixture.detectChanges();

    const taskElements = fixture.nativeElement.querySelectorAll('.task-item');
    expect(taskElements.length).toBe(2);
  });

  it('debería mostrar un estado vacío cuando no hay tareas', () => {
    component.tasks = [];
    component.loading = false;
    fixture.detectChanges();

    const emptyState = fixture.nativeElement.querySelector('.empty-state');
    expect(emptyState).toBeTruthy();
  });

  describe('onToggleTask', () => {
    it('debería alternar el estado de finalización de la tarea y emitir un evento', fakeAsync(() => {
      // Mock de la tarea actualizada que devuelve el servicio
      const updatedTaskFromService: TaskRequest = {
        id: 1,
        title: 'Task 1',
        description: 'Desc 1',
        dateOfCreation: '2023-01-01',
        completed: true // Estado cambiado
      };

      // Configurar el spy para que devuelva el mock correctamente tipado
      spyOn(taskService, 'updateTask').and.returnValue(of(updatedTaskFromService));
      spyOn(component.taskUpdated, 'emit');

      // Tarea original (completed: false)
      const originalTask = mockTasks[0];
      component.onToggleTask(originalTask);

      // Verificar que se llamó al servicio con los parámetros correctos
      expect(taskService.updateTask).toHaveBeenCalledWith({
        ...originalTask,
        completed: true // Debería haber cambiado a true
      });

      // Verificar que se emitió el evento con la tarea actualizada
      expect(component.taskUpdated.emit).toHaveBeenCalledWith(updatedTaskFromService);
    }));
  });


  describe('onDeleteTask', () => {
    it('debería llamar al servicio de eliminación y emitir un evento', fakeAsync(() => {
      spyOn(taskService, 'deleteTask').and.returnValue(of(undefined));
      spyOn(component.deleteTask, 'emit');

      const mockEvent = { stopPropagation: jasmine.createSpy() } as unknown as Event;
      component.onDeleteTask(mockEvent, 1);

      expect(mockEvent.stopPropagation).toHaveBeenCalled();
      expect(taskService.deleteTask).toHaveBeenCalledWith(1);
      expect(component.deleteTask.emit).toHaveBeenCalledWith(1);
    }));
  });

});
