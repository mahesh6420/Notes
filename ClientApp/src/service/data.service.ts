import { Inject, Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Note } from "src/interface/note";
import {map} from 'rxjs/operators';
import { DataResult, DataResultWithT } from "src/interface/data-result";
@Injectable({
  providedIn: 'root'
})
export class DataService<T> {
  controllerName: string;
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseurl: string) {
    this.baseurl = `${this.baseurl}api/`;
  }

  getAll() {
    return this.http.get(`${this.baseurl}${this.controllerName}`)
                      .pipe(map((response: Response) => response));
  }

  get(id: number) {
    return this.http.get(`${this.baseurl}${this.controllerName}/${id}`);
  }

  create(data: T) {
    return this.http.post(`${this.baseurl}${this.controllerName}/`, data)
                .pipe(map((response) => response as DataResultWithT<T>));
  }

  update(data: T) {
    return this.http.patch(`${this.baseurl}${this.controllerName}/`, data);
  }

  delete(data: T) {
    return this.http.delete(`${this.baseurl}${this.controllerName}/${data['id']}`)
                    .pipe(map((response) => response as DataResult));
  }
}
