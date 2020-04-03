import { Inject, Injectable, Query } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import {map} from 'rxjs/operators';
import { DataResult, DataResultWithT } from "src/interface/data-result";
import { ResultStatus } from "src/common/enum/result-status";
import { ToastrService } from "ngx-toastr";
import { QueryParam } from "src/interface/query-param";
@Injectable({
  providedIn: 'root'
})
export class DataService<T> {
  controllerName: string;

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseurl: string,
    private toastr: ToastrService) {
    this.baseurl = `${this.baseurl}api/`;
  }

  getTotalCount(take: number) {
    return this.http.get(`${this.baseurl}${this.controllerName}/totalCount`)
    .pipe(map((response: number) => {
      return {
        totalPage: Math.ceil(response / take),
        totalCount: response
      };
    }));
  }

  getAll(queryParam?: QueryParam) {
    queryParam = queryParam || new QueryParam;
    const options =  { params: new HttpParams({ fromObject: JSON.parse(JSON.stringify(queryParam)) })};
    return this.http.get(`${this.baseurl}${this.controllerName}`, options)
                      .pipe(map((response: Response) => response));
  }

  get(id: number) {
    return this.http.get(`${this.baseurl}${this.controllerName}/${id}`);
  }

  create(data: T) {
    return this.http.post(`${this.baseurl}${this.controllerName}/`, data)
                .pipe(map((response) => {
                  const res = response as DataResultWithT<T>;
                  if (res.status === ResultStatus.Success) {
                    this.toastr.success(res.message, `${this.controllerName.charAt(0).toUpperCase()}${this.controllerName.slice(1)}`);
                  } else {
                    this.toastr.error(res.message, `${this.controllerName.charAt(0).toUpperCase()}${this.controllerName.slice(1)}`);
                  }

                  return res;
                }));
  }

  update(data: T) {
    return this.http.patch(`${this.baseurl}${this.controllerName}/`, data)
              .pipe(map((response) => {
                const res = response as DataResultWithT<T>;
                if (res.status === ResultStatus.Success) {
                  this.toastr.success(res.message, `${this.controllerName.charAt(0).toUpperCase()}${this.controllerName.slice(1)}`);
                } else {
                  this.toastr.error(res.message, `${this.controllerName.charAt(0).toUpperCase()}${this.controllerName.slice(1)}`);
                }

                return res;
              }));
  }

  delete(data: T) {
    return this.http.delete(`${this.baseurl}${this.controllerName}/${data['id']}`)
                    .pipe(map((response) => {
                     const res = response as DataResult;
                     if (res.status === ResultStatus.Success) {
                       this.toastr.success(res.message, `${this.controllerName.charAt(0).toUpperCase()}${this.controllerName.slice(1)}`);
                     }
                     return res;
                    }));
  }
}
