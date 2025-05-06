import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-auth.layout',
  imports: [RouterOutlet],
  template: `<div
    class="min-h-screen flex  justify-center"
  >
    <div>
      <h1 class="text-4xl text-gray-700 my-14 tracking-[6px] font-semibold text-center">
        <span class="text-blue-500">Task</span>ify
      </h1>
      <router-outlet />
    </div>
  </div>`,
})
export default class AuthLayoutComponent {}
