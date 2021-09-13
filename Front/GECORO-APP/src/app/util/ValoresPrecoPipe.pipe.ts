import { DecimalPipe } from '@angular/common';
import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'ValoresPrecoPipe'
})
export class ValoresPrecoPipePipe extends DecimalPipe implements PipeTransform {

  transform(value: any, args?: any): any {
  }

}
