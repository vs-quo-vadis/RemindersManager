import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';

export function getBaseUrl() {
  let baseUrl = document.getElementsByTagName('base')[0].href;

  if(baseUrl.includes('4200')){
    return 'http://localhost:53860/';
  }else{
    return baseUrl;
  } 
}

if (environment.production) {
  enableProdMode();
}

const providers = [
  { provide: 'BASE_URL', useFactory: getBaseUrl, deps: [] }
];

platformBrowserDynamic(providers).bootstrapModule(AppModule)
  .catch(err => console.error(err));
