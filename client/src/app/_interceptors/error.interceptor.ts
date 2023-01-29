// Questo codice è un intercettatore HTTP in Angular che gestisce gli errori delle richieste HTTP. 
// Il costruttore della classe inizializza due dipendenze, Router e ToastrService. 
// Il metodo intercept() utilizza il metodo pipe() di RxJS per intercettare eventuali errori nella richiesta HTTP. 
// In caso di errore, il codice controlla il codice di stato dell'errore (ad esempio, 400, 401, 404, 500) e gestisce l'errore 
// in modo appropriato. 
// Ad esempio, se il codice di stato è 400 e ci sono errori nel 
// campo "errori", vengono estratti questi errori e vengono visualizzati come messaggi di errore. 
// Se il codice di stato è 401, viene visualizzato un messaggio di errore "Non autorizzato". 
// Se il codice di stato è 404, l'utente viene reindirizzato alla pagina "non trovata". 
// Se il codice di stato è 500, l'utente viene reindirizzato alla pagina "errore del server" 
// e l'errore viene passato come stato. In caso contrario, viene visualizzato un messaggio di errore generico 
// e l'errore viene stampato nel console. In generale l'intercettatore gestisce gli errori restituiti dalle richieste 
// http e mostra messaggi di errore in base all'errore restituito.

import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { catchError, Observable } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  //Inizializza l'oggetto router e toastr
  constructor(private router: Router, private toastr: ToastrService) { }

  //Intercetta la richiesta http e gestisce eventuali errori
  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        // Se c'è un errore, esegue uno switch sullo stato dell'errore
        if (error) {
          switch (error.status) {
            case 400:
              // Se ci sono errori nell'oggetto error.error, itera su di essi e li inserisce in un array modalStateErrors
              if (error.error.errors) {
                const modalStateErrors = [];
                for (const key in error.error.errors) {
                  if (error.error.errors[key]) {
                    modalStateErrors.push(error.error.errors[key])
                  }
                }
                // Lancia gli errori in modo che possano essere gestiti da chi chiama questa funzione. Il flat serve a unire due array in uno
                throw modalStateErrors.flat();
              } else {
                // Se non ci sono errori specifici, mostra un messaggio di errore generico utilizzando la funzione toastr.error()
                this.toastr.error(error.error, error.status.toString())
              }
              break;
            case 401:
              // Se lo stato è 401 (non autorizzato), mostra un messaggio di errore
              this.toastr.error('Unauthorized', error.status.toString())
              break;
            case 404:
              // Se lo stato è 404 (pagina non trovata), reindirizza alla pagina di errore 404
              this.router.navigateByUrl('/not-found');
              break;
            case 500:
              // Se lo stato è 500 (errore del server), reindirizza alla pagina di errore del server e passa l'oggetto error come stato
              const navigationExtras: NavigationExtras = {
                state: {
                  error: error.error
                }
              }
              this.router.navigateByUrl('/server-error', navigationExtras);
              break;
            default:
              // Se non si tratta di nessuno degli stati elencati sopra, mostra un messaggio di errore generico e logga l'errore
              this.toastr.error('Something unexpected went wrong')
              console.log(error);
              break;
          }
        }
        // Lancia l'errore per essere gestito da chi chiama questa funzione
        throw error;
      })
    )
  }
}
