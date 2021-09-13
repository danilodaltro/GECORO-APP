import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-vendedores-detalhes',
  templateUrl: './vendedores-detalhes.component.html',
  styleUrls: ['./vendedores-detalhes.component.css']
})
export class VendedoresDetalhesComponent implements OnInit {
  form: FormGroup = new FormGroup({});

  get f(): any{
    return this.form.controls;
  }

  constructor(private fb: FormBuilder) {
   }

  ngOnInit() {
    this.validation();
  }

  private validation(): void{
    this.form = this.fb.group({
      nome: ['',Validators.required],
      codigo: ['',[Validators.required, Validators.minLength(5), Validators.maxLength(5),
                Validators.pattern("[0-9]{5}")]]
    });
  }
}
