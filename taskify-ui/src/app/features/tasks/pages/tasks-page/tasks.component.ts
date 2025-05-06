import {Component} from '@angular/core';
import { TaskListComponent } from '../../components/task-list/task-list.component';

@Component({
  selector: 'app-tasks-page',
  imports: [TaskListComponent],
  templateUrl: './tasks-page.component.html',
})
export default class TasksPageComponent {
}
