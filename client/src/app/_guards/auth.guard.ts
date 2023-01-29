import { Injectable } from '@angular/core';
import { CanActivate} from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { map, Observable } from 'rxjs';
import { AccountService } from '../_services/account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private accountService: AccountService, private toastr: ToastrService ) {}
  
  canActivate(): Observable<boolean> {
    // utilizziamo il metodo pipe sull'observable currentUser$ del servizio accountService
    return this.accountService.currentUser$.pipe(
      // utilizziamo la funzione map per eseguire una funzione su ogni valore emesso dall'observable
      map(user => {
        // se l'utente è presente, restituiamo true
        if(user) return true;
        // altrimenti, mostriamo un messaggio di errore tramite il toastr e restituiamo false
        else {
          this.toastr.error('You are not logged');
          return false;
        }
      })
    )
  }

  
}
