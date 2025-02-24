import { Component, EventEmitter, Output } from '@angular/core';
import { IClassDetails } from './classDetails';
import { CLASSLIST } from './mock-list';


@Component({
  selector: 'app-class-list',
  templateUrl: './class-list.component.html',
  styleUrls: ['./class-list.component.css']
})

export class ClassListComponent {
  classes = CLASSLIST;
  selectedClass?:IClassDetails;
  selectedClasses?:IClassDetails[] = [];
  
  @Output() selectionOutput = new EventEmitter<IClassDetails[]>();

  onSelect(classDetails: IClassDetails): void {
    this.selectedClass=classDetails;
    if(this.selectedClasses?.indexOf(classDetails) !== undefined){
      const foundClass: number = this.selectedClasses.indexOf(classDetails)
      if(foundClass === -1){
      this.selectedClasses.push(classDetails);
      this.selectionOutput.emit(this.selectedClasses);
      }
      else{
        this.selectedClasses.splice(foundClass, 1);
        this.selectionOutput.emit(this.selectedClasses);
      }
  }
}
  isSelectedArray(classDetails: IClassDetails){
    if(this.selectedClasses?.indexOf(classDetails) !== undefined){
      if(this.selectedClasses?.indexOf(classDetails) >= 0){
        return true;
      }
      else 
        return false;
    }
    else
      return false;
}
}

