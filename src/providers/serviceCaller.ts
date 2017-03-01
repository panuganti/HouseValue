import {Injectable} from '@angular/core';
import {Http, Headers} from '@angular/http';
import {Observable} from 'rxjs/Observable';
import { Property } from '../models/Property';
//import 'rxjs/add/observable/fromArray'; // required for Observable.of();
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/retry';

@Injectable()
export class ServiceCaller {
    //url: string = "https://script.google.com/macros/s/AKfycbz2ZMnHuSR4GmTjsuIo6cmh433RRpPRH7TwMaJhbAUr/dev";
    url: string = "http://localhost:54909";

    constructor(private http: Http) {
    }
  
    getEstimate(property:Property) : Observable<number>
    {
        return this.postRequest<number>("/feed/getestimate", JSON.stringify(property));
    }

    getLatLong(address: string): Observable<any>
    {
        var gUrl = 'https://maps.googleapis.com/maps/api/geocode/json?address='+address+'&key=AIzaSyDMzbsjFw3JFcAqpOnav8IGXqInxmLPY4M';
        return this.http.get(gUrl).map(res => res.json());
    }

   //#region Class
    getRequest<T>(route: string, request: string, retryCount: number = 0) : Observable<T> {
        var headers = new Headers();
        headers.append('Content-Type', 'application/json');
        return this.http.get(this.url + route + request, { headers: headers })
                        .retry(retryCount)
                        .map(res => res.json());                        
    }
    
    postRequest<T>(route: string, jsonString: string, retryCount: number = 0) : Observable<T> {
        var headers = new Headers();
        headers.append('Content-Type', 'application/json');
        return this.http.post(this.url + route, jsonString, { headers: headers })
                        .retry(retryCount)
                        .map(res => res.json());
    }
   //#endregion Class
}