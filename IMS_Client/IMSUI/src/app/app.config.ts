import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter, withHashLocation } from '@angular/router';

import { HTTP_INTERCEPTORS, provideHttpClient, withFetch } from '@angular/common/http'

import { routes } from './app.routes';
import { provideClientHydration } from '@angular/platform-browser';
import { AuthServiceService } from './Services/Auth/auth-service.service';

export const appConfig: ApplicationConfig = {
  providers: [provideZoneChangeDetection({ eventCoalescing: true }),
     provideRouter(routes), 
     provideClientHydration(),
     provideRouter(routes),provideHttpClient(withFetch(),),
    {provide:HTTP_INTERCEPTORS,useClass:AuthServiceService,multi:true}
  ]
};
