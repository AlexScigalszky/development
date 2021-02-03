import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-padlock',
  templateUrl: './padlock.component.html',
})
export class PadlockComponent {
  @Input() isOpen: boolean;
}
