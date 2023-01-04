import { Component } from '@angular/core';

import '@cds/core/icon/register.js';
import { loadCoreIconSet} from '@cds/core/icon';

loadCoreIconSet();

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'TimeRegistration App';
}
