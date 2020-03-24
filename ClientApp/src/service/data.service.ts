import { Inject, Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Note } from "src/interface/note";
import {map} from 'rxjs/operators';
@Injectable({
  providedIn: 'root'
})
export class DataService {
  controllerName: string;
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseurl: string) {
  }

  getAll() {
    return this.http.get(`${this.baseurl}${this.controllerName}`)
                      .pipe(map((response: Response) => response));
  }

  get(id: number) {
    return this.http.get(`${this.baseurl}${this.controllerName}/${id}`);
  }

  save(note: Note) {
    return this.http.post(`${this.baseurl}${this.controllerName}/`, note);
  }

  update(note: Note) {
    return this.http.patch(`${this.baseurl}${this.controllerName}/`, note);
  }

  delete(note: Note) {
    return this.http.delete(`${this.baseurl}${this.controllerName}/${note.id}`);
  }
}
