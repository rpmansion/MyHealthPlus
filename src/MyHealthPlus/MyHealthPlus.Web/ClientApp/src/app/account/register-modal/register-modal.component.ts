import { Component, OnInit, Inject } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-register-modal',
  templateUrl: './register-modal.component.html',
  styleUrls: ['./register-modal.component.scss']
})
export class RegisterModalComponent implements OnInit {
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
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      birthDate: ['', Validators.required],
      email: ['', Validators.required],
      middleName: '',
      contact: ''
    });
  }

  handleFormSubmit(data: any) {
    this.register(data).subscribe(
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
  register(data: any): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}api/account/register`, data);
  }
}
