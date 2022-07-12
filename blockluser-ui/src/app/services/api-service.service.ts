import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { SigninRequest } from '../contacts/signin-request';
import { SignupRequest } from '../contacts/signup-request';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  constructor(private http: HttpClient) {}

  public async GetCatalog(): Promise<any> {
    const options = this.HttpOptions();
    const result = await this.http
      .get<string>(`${environment.apiUrl}/GetCatalog`)
      .toPromise();
    return result;
  }

  public async SignIn(signinRequest: SigninRequest): Promise<any> {
    const options = this.HttpOptions();
    const result = await this.http.post<any>(
      `${environment.apiUrl}/SignIn`,
      JSON.stringify(signinRequest)
    );
    try {
      const returnResult = await result.toPromise();
      if (returnResult.Success) {
        localStorage['token'] = returnResult.Token;
        return true;
      }
      return false;
    } catch {
      return false;
    }
  }

  public async SignUp(signupRequest: SignupRequest): Promise<boolean> {
    const options = this.HttpOptions();
    const result = await this.http.post<any>(
      `${environment.apiUrl}/SignUp`,
      JSON.stringify(signupRequest)
    );
    try {
      const returnResult = await result.toPromise();
      if (returnResult.Success) {
        localStorage['token'] = returnResult.Token;
        return true;
      }
      return false;
    } catch {
      return false;
    }
  }

  public async SignOut(): Promise<boolean> {
    // const options = this.HttpOptions();
    // const result = await this.http.post<any>(
    //   `${environment.apiUrl}/SignUp`,
    //   JSON.stringify(signupRequest)
    // );
    // try {
    //   const returnResult = await result.toPromise();
    //   if (returnResult.Success) {
    //     localStorage['token'] = returnResult.Token;
    //     return true;
    //   }
    //   return false;
    // } catch {
    //   return false;
    // }
    console.log('log out');
    localStorage['token'] = undefined;
    localStorage.clear();
    return true;
  }

  public IsLoggedIn(): boolean {
    var token = localStorage['token'];
    if (!token) {
      return false;
    } else {
      return true;
    }
  }
  private HttpOptions(): { headers: HttpHeaders } {
    return {
      headers: new HttpHeaders({
        Accept: 'application/json',
        Authorization: `Bearer ${'token'}`,
      }),
    };
  }
}
