import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {TaskRequest} from '../interface/TaskRequest';
import {environment} from '../../../../environments/environment';

@Injectable({providedIn: 'root'})
export class TaskService {
  private readonly httpClient = inject(HttpClient);

  getTasks() {
    return this.httpClient.get<TaskRequest[]>(`${environment.apiUrl}/tasks`, {withCredentials: true});
  }

}
