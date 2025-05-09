import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {TaskRequest} from '../interface/TaskRequest';

@Injectable({providedIn: 'root'})
export class TaskService {
  private readonly httpClient = inject(HttpClient);
  private apiUrl = 'http://localhost:5000/api';

  getTasks() {
    return this.httpClient.get<TaskRequest[]>(`${this.apiUrl}/tasks`, {withCredentials: true});
  }

}
