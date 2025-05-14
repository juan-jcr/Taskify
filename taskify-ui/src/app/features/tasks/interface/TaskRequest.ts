export interface TaskRequest {
  id: number;
  title: string;
  description: string;
  dateOfCreation:null | string;
  completed: boolean;
}
