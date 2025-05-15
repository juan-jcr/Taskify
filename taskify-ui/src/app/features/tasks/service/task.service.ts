import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {TaskRequest} from '../interface/TaskRequest';
import {environment} from '../../../../environments/environment';
import {catchError, Observable, tap, throwError} from 'rxjs';

@Injectable({providedIn: 'root'})
export class TaskService {
  private readonly httpClient = inject(HttpClient);

  getTasks(): Observable<TaskRequest[]> {
    return this.httpClient.get<TaskRequest[]>(`${environment.apiUrl}/tasks`, {withCredentials: true});
  }
   addTask(taskRequest: TaskRequest): Observable<TaskRequest> {
    return this.httpClient.post<TaskRequest>(`${environment.apiUrl}/tasks`, taskRequest, {withCredentials: true});
   }

  updateTask(taskRequest: Partial<TaskRequest>): Observable<TaskRequest> {
    return this.httpClient.put<TaskRequest>(
      `${environment.apiUrl}/tasks/${taskRequest.id}`, taskRequest, { withCredentials: true
      })

  }

}
