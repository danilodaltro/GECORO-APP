import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-clientes-detalhes',
  templateUrl: './clientes-detalhes.component.html',
  styleUrls: ['./clientes-detalhes.component.css']
})
export class ClientesDetalhesComponent implements OnInit {

  form: FormGroup = new FormGroup({});
  constructor(private fb: FormBuilder) { }

  ngOnInit() {
    this.validation();
  }

  get f(): any{
    return this.form.controls;
  }

  public validation(): void{
    this.form = this.fb.group({
      nome: ['',Validators.required],
      cpf: ['',[Validators.required, Validators.minLength(11), Validators.maxLength(11),
                Validators.pattern("[0-9]{11}")]]
    })
  }
}
