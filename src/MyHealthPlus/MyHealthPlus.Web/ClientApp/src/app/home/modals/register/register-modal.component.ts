import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-register-modal',
  templateUrl: './register-modal.component.html',
  styleUrls: ['./register-modal.component.scss']
})
export class RegisterModalComponent implements OnInit {
  registerForm: FormGroup;
  constructor(public activeModal: NgbActiveModal,
    private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.createForm();
  }

  private createForm() {
    this.registerForm = this.formBuilder.group({
      firstName: '',
      lastName: '',
      middleName: '',
      birthDate: '',
      email: '',
      contact: ''
    });
  }
  private submitForm() {
    // TODO : Call an API to register
    this.activeModal.close(this.registerForm.value);
  }

  closeModal() {
    this.activeModal.close('Modal Closed');
  }
}
