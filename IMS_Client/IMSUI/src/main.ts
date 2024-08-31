import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app.component';
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { AuthServiceService } from './app/Services/Auth/auth-service.service';


bootstrapApplication(AppComponent,{providers: [ ...appConfig.providers,
  provideHttpClient(
    withInterceptorsFromDi(),
  ),
  {provide: HTTP_INTERCEPTORS, useClass:AuthServiceService, multi: true},
]})
  .catch((err) => console.error(err));
