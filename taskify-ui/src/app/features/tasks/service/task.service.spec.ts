import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TaskService } from './task.service';
import {TaskRequest} from "../interface/TaskRequest";
import {environment} from "../../../../environments/environment";


describe('TaskService', () => {
    let service: TaskService;
    let httpMock: HttpTestingController;

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpClientTestingModule],
            providers: [TaskService]
        });
        service = TestBed.inject(TaskService);
        httpMock = TestBed.inject(HttpTestingController);
    });

    afterEach(() => {
        httpMock.verify();
    });

    it('debería ser creado', () => {
        expect(service).toBeTruthy();
    });

    describe('getTasks', () => {
        it('debería devolver tareas de la API', () => {
            const mockTasks: TaskRequest[] = [
                { id: 1, title: 'Task 1', description: 'Desc 1', dateOfCreation: '2023-01-01', completed: false },
                { id: 2, title: 'Task 2', description: 'Desc 2', dateOfCreation: '2023-01-02', completed: true }
            ];

            service.getTasks().subscribe(tasks => {
                expect(tasks).toEqual(mockTasks);
            });

            const req = httpMock.expectOne(`${environment.apiUrl}/tasks`);
            expect(req.request.method).toBe('GET');
            expect(req.request.withCredentials).toBeTrue();
            req.flush(mockTasks);
        });

        it('debería manejar la respuesta vacía', () => {
            service.getTasks().subscribe(tasks => {
                expect(tasks).toEqual([]);
            });

            const req = httpMock.expectOne(`${environment.apiUrl}/tasks`);
            req.flush([]);
        });
    });

    describe('addTask', () => {
        it('debería enviar una solicitud POST al endpoint correcto con los datos de la tarea', () => {
            const mockTask: TaskRequest = {
                id: 1,
                title: 'Test Task',
                description: 'Test Description',
                dateOfCreation: '06/05/2024',
                completed: false
            };

            service.addTask(mockTask).subscribe((response) => {
                expect(response).toEqual(mockTask);
            });

            const req = httpMock.expectOne(`${environment.apiUrl}/tasks`);
            expect(req.request.method).toBe('POST');
            expect(req.request.body).toEqual(mockTask);
            expect(req.request.withCredentials).toBeTrue();

            req.flush(mockTask);
        });

        it('debería manejar errores cuando la solicitud falla', () => {
            const mockTask: TaskRequest = {
                id: 1,
                title: 'Test Task',
                description: 'Test Description',
                dateOfCreation: '06/05/2024',
                completed: false
            };
            const mockError = { status: 500, statusText: 'Server Error' };

            service.addTask(mockTask).subscribe({
                next: () => fail('should have failed with 500 error'),
                error: (error) => {
                    expect(error.status).toEqual(500);
                }
            });

            const req = httpMock.expectOne(`${environment.apiUrl}/tasks`);
            req.flush('Error', mockError);
        });

    });

    describe('deleteTask', () => {
        it('debería enviar una solicitud DELETE', () => {
            const taskId = 123;
            let receivedValue = false;

            service.deleteTask(taskId).subscribe({
                next: () => receivedValue = true,
                error: () => fail('should not fail')
            });

            const req = httpMock.expectOne(`${environment.apiUrl}/tasks/${taskId}`);
            req.flush(null);

            expect(receivedValue).toBeTrue();
        });



        it('debería incluir el ID de la tarea en la URL', () => {
            const taskId = 456;

            service.deleteTask(taskId).subscribe();

            const req = httpMock.expectOne(`${environment.apiUrl}/tasks/${taskId}`);
            expect(req.request.url).toContain(`/tasks/${taskId}`);
            req.flush(null);
        });

        it('debe manejar errores cuando la eliminación de una tarea falla', () => {
            const taskId = 789;
            const mockError = { status: 404, statusText: 'Not Found' };

            service.deleteTask(taskId).subscribe({
                next: () => fail('should have failed with 404 error'),
                error: (error) => {
                    expect(error.status).toEqual(404);
                }
            });

            const req = httpMock.expectOne(`${environment.apiUrl}/tasks/${taskId}`);
            req.flush('Error', mockError);
        });
    });

    describe('updateTask', () => {
        it('debería enviar una solicitud PUT al endpoint correcto con los datos de la tarea', () => {
            const mockTask: Partial<TaskRequest> = {
                id: 1,
                title: 'Updated Task',
                completed: true
            };

            const expectedResponse: TaskRequest = {
                id: 1,
                title: 'Updated Task',
                description: 'Original Description', // El backend podría mantener valores no actualizados
                dateOfCreation: '2023-01-01',
                completed: true
            };

            service.updateTask(mockTask).subscribe((response) => {
                expect(response).toEqual(expectedResponse);
            });

            const req = httpMock.expectOne(`${environment.apiUrl}/tasks/${mockTask.id}`);
            expect(req.request.method).toBe('PUT');
            expect(req.request.body).toEqual(mockTask);
            expect(req.request.withCredentials).toBeTrue();

            req.flush(expectedResponse);
        });



        it('debería manejar errores cuando la solicitud de actualización falla', () => {
            const mockTask: Partial<TaskRequest> = {
                id: 1,
                title: 'Updated Task'
            };
            const mockError = { status: 404, statusText: 'Not Found' };

            service.updateTask(mockTask).subscribe({
                next: () => fail('should have failed with 404 error'),
                error: (error) => {
                    expect(error.status).toEqual(404);
                }
            });

            const req = httpMock.expectOne(`${environment.apiUrl}/tasks/${mockTask.id}`);
            req.flush('Not Found', mockError);
        });
    });

});
