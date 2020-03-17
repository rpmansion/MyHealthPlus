import { Component, OnInit, Inject } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-login-modal',
  templateUrl: './login-modal.component.html',
  styleUrls: ['./login-modal.component.scss']
})
export class LoginModalComponent implements OnInit {
  form: FormGroup;

  constructor(
    private http: HttpClient,
    private activeModal: NgbActiveModal,
    @Inject('BASE_URL') private baseUrl: string,
    private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.createForm();
  }

  private createForm() {
    this.form = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  handleFormSubmit(data: any) {
    this.login(data).subscribe(
      response => {
        console.log(response);
        // this.activeModal.close();
      }
    );
  }

  closeModal() {
    this.activeModal.close('Modal Closed');
  }

  // TODO : move this to service
  login(data: any): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}api/account/login`, data);
  }
}
