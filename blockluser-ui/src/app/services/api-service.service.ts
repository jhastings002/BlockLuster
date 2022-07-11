import { HttpClient, HttpHeaders} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ApiService {

  constructor(private http: HttpClient) { }

  public async GetCatalog(): Promise<any>{
    const options = this.HttpOptions();
    const result = await this.http.get<string>(`${environment.apiUrl}/GetCatalog`).toPromise();
    return result
  }

  private HttpOptions(): { headers: HttpHeaders} {
    return {
      headers: new HttpHeaders({
        Accept: 'application/json',
        Authorization: `Bearer ${'token'}`
      })
    }
  }
}
