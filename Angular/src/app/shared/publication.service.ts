import { Injectable } from "@angular/core";
import { Publication } from "./publication.model";
import { HttpClient } from "@angular/common/http";
import { environment } from "src/environments/environment";

@Injectable({
  providedIn: "root",
})
export class PublicationService {
  formData: Publication;

  constructor(private http: HttpClient) {}

  saveOrUpdateCategorie() {
    var body = {
      ...this.formData,
    };
    if (body.PublicationID == null)
      return this.http.post(environment.apiURL + "/Publication", body);
    return this.http.put(
      environment.apiURL + "/Publication/" + body.PublicationID,
      body
    );
  }

  saveOrUpdatePublicationV02() {
    var body = {
      ...this.formData,
    };
    return this.http
      .post(environment.apiURL + "/Publication", body)
      .toPromise();
  }

  getPublicationList() {
    return this.http.get(environment.apiURL + "/Publication").toPromise();
  }
  getPublicationByID(id: number): any {
    return this.http.get(environment.apiURL + "/Publication/" + id).toPromise();
  }
}
