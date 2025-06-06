import { ComponentFixture, TestBed } from '@angular/core/testing';
import { TaskAddComponent } from './task-add.component';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import {TaskService} from '../../service/task.service';
import {TaskRequest} from '../../interface/TaskRequest';

describe('TaskAddComponent', () => {
  let component: TaskAddComponent;
  let fixture: ComponentFixture<TaskAddComponent>;
  let taskService: jasmine.SpyObj<TaskService>;
  let formBuilder: FormBuilder;

  beforeEach(async () => {
    const taskServiceSpy = jasmine.createSpyObj('TaskService', ['addTask']);

    await TestBed.configureTestingModule({
      imports: [
        ReactiveFormsModule,
        HttpClientTestingModule,
        TaskAddComponent // Importamos el componente standalone
      ],
      providers: [
        FormBuilder,
        { provide: TaskService, useValue: taskServiceSpy }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(TaskAddComponent);
    component = fixture.componentInstance;
    taskService = TestBed.inject(TaskService) as jasmine.SpyObj<TaskService>;
    formBuilder = TestBed.inject(FormBuilder);

    fixture.detectChanges();
  });

  it('debe crear', () => {
    expect(component).toBeTruthy();
  });

  describe('Inicialización del formulario', () => {
    it('debería inicializar el formulario con los campos requeridos', () => {
      expect(component.taskForm).toBeDefined();
      expect(component.taskForm.contains('title')).toBeTruthy();
      expect(component.taskForm.contains('description')).toBeTruthy();
      expect(component.taskForm.contains('dateOfCreation')).toBeTruthy();

      const titleControl = component.taskForm.get('title');
      expect(titleControl?.hasValidator(Validators.required)).toBeTrue();
    });
  });

  describe('submit()', () => {
    it('no se debe llamar a addTask si el formulario es inválido', () => {
      component.taskForm.get('title')?.setValue('');
      component.submit();
      expect(taskService.addTask).not.toHaveBeenCalled();
    });

    it('debería llamar a addTask con los valores del formulario cuando el formulario sea válido', () => {
      const mockTask: TaskRequest = {
        title: 'Test Task',
        description: 'Test Description',
        dateOfCreation: '06/05/2024',
        completed: false,
        id: 0
      };

      component.taskForm.patchValue({
        title: mockTask.title,
        description: mockTask.description,
        dateOfCreation: mockTask.dateOfCreation
      });

      taskService.addTask.and.returnValue({
        subscribe: (callbacks: any) => {
          callbacks.next();
        }
      } as any);

      component.submit();

      expect(taskService.addTask).toHaveBeenCalledWith(jasmine.objectContaining({
        title: mockTask.title,
        description: mockTask.description,
        dateOfCreation: mockTask.dateOfCreation
      }));
    });

  });

  describe('Validación de formulario', () => {
    it('debería marcar el título como inválido cuando esté vacío', () => {
      const titleControl = component.taskForm.get('title');
      titleControl?.setValue('');
      expect(titleControl?.valid).toBeFalse();
      expect(titleControl?.errors?.['required']).toBeTruthy();
    });
    it('deberia marcar la fecha de creación como inválido cuando esté vacío', ( ) => {
      const dateOfCreation = component.taskForm.get('dateOfCreation');
      dateOfCreation?.setValue('');
      expect(dateOfCreation?.valid).toBeFalse();
      expect(dateOfCreation?.errors?.['required']).toBeTruthy();
    })

    it('debería marcar el título como válido cuando no esté vacío', () => {
      const titleControl = component.taskForm.get('title');
      titleControl?.setValue('Valid Title');
      expect(titleControl?.valid).toBeTrue();
    });
    it('debería marcar fecha de creación como válido cuando no está vacío' , () => {
      const dateOfCreation = component.taskForm.get('dateOfCreation');
      dateOfCreation?.setValue('09/05/2024');
      expect(dateOfCreation?.valid).toBeTrue();
    })
  });
});
