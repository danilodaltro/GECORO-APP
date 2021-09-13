import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-titulo',
  templateUrl: './titulo.component.html',
  styleUrls: ['./titulo.component.css']
})
export class TituloComponent implements OnInit {

  @Input() titulo = "";
  @Input() iconClass = "fa fa-user";
  @Input() botaoListar = false;
  @Input() bgColor = "secondary";
  constructor(private router: Router) { }

  ngOnInit() {
  }

  listar(): void{
    this.router.navigate([`${this.titulo.toLowerCase()}/lista`]);
  }
}
