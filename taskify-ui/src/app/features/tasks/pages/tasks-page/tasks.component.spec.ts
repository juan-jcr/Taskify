import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { of, throwError } from 'rxjs';
import {TaskRequest} from "../../interface/TaskRequest";
import {TaskService} from "../../service/task.service";
import TasksPageComponent from "./tasks.component";

describe('TasksPageComponent', () => {
    let component: TasksPageComponent;
    let fixture: ComponentFixture<TasksPageComponent>;
    let taskService: TaskService;

    const mockTasks: TaskRequest[] = [
        { id: 1, title: 'Task 1', description: 'Desc 1', dateOfCreation: '2023-01-01', completed: false },
        { id: 2, title: 'Task 2', description: 'Desc 2', dateOfCreation: '2023-01-02', completed: true }
    ];

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [HttpClientTestingModule, TasksPageComponent],
            providers: [TaskService]
        }).compileComponents();

        fixture = TestBed.createComponent(TasksPageComponent);
        component = fixture.componentInstance;
        taskService = TestBed.inject(TaskService);
        fixture.detectChanges();
    });

    it('debería crear', () => {
        expect(component).toBeTruthy();
    });

    it('debería inicializarse con el estado de carga', () => {
        expect(component.loading()).toBeTrue();
    });

    describe('loadTasks', () => {
        it('debería cargar tareas con éxito', fakeAsync(() => {
            spyOn(taskService, 'getTasks').and.returnValue(of(mockTasks));

            component.loadTasks();
            tick();

            expect(component.tasks()).toEqual(mockTasks);
            expect(component.loading()).toBeFalse();

        }));

    });

    it('debería llamar a loadTasks en la inicialización', () => {
        spyOn(component, 'loadTasks');
        component.ngOnInit();
        expect(component.loadTasks).toHaveBeenCalled();
    });

  describe('onTaskUpdated', () => {
    it('debería actualizar la tarea correcta en el array de tareas', () => {
      const initialTasks: TaskRequest[] = [
        { id: 1, title: 'Task 1', description: 'Desc 1', dateOfCreation: '2023-01-01', completed: false },
        { id: 2, title: 'Task 2', description: 'Desc 2', dateOfCreation: '2023-01-02', completed: false }
      ];

      component.tasks.set(initialTasks);

      const updatedTask: TaskRequest = {
        id: 1,
        title: 'Updated Task 1',
        description: 'Updated Desc 1',
        dateOfCreation: '2023-01-01',
        completed: true
      };

      component.onTaskUpdated(updatedTask);

      expect(component.tasks().length).toBe(2);
      expect(component.tasks()[0].title).toBe('Updated Task 1');
      expect(component.tasks()[0].completed).toBe(true);
      expect(component.tasks()[1]).toEqual(initialTasks[1]); // Verifica que la otra tarea no cambió
    });
  });

  describe('onTaskSelected', () => {
    it('debería establecer la tarea seleccionada', () => {
      const task: TaskRequest = {
        id: 1,
        title: 'Test Task',
        description: 'Test Desc',
        dateOfCreation: '2023-01-01',
        completed: false
      };

      component.onTaskSelected(task);

      expect(component.selectedTask()).toEqual(task);
    });
  });
});
