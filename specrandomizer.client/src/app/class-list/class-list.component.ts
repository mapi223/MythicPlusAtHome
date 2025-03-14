import { ChangeDetectorRef, Component, EventEmitter, Input, Output } from '@angular/core';
import { IClassDetails } from './classDetails';
import { CLASSLIST } from './mock-list';


@Component({
  selector: 'app-class-list',
  templateUrl: './class-list.component.html',
  styleUrls: ['./class-list.component.css']
})

export class ClassListComponent {
  classes = CLASSLIST;
  selectedClasses: IClassDetails[] = [];

  @Input() passedSelected: number[] = [];
  @Output() selectionOutput = new EventEmitter<IClassDetails[]>();

  constructor(private cdr: ChangeDetectorRef) { }

  ngAfterViewInit() {
    if (this.passedSelected && this.passedSelected.length) {
      this.passedSelected.forEach(selectedId => {
        const classDetails = this.classes.find(c => c.id === selectedId);
        if (classDetails) {
          this.onSelect(classDetails);
        }
      });
      this.cdr.detectChanges();
    }
  }

  onSelect(classDetails: IClassDetails): void {
    const foundIndex = this.selectedClasses.findIndex(c => c.id === classDetails.id);

    if (foundIndex === -1) {
      this.selectedClasses.push(classDetails);
    } else {
      this.selectedClasses.splice(foundIndex, 1);
    }

    this.selectionOutput.emit([...this.selectedClasses]);
  }

  isSelected(classDetails: IClassDetails): boolean {
    return this.selectedClasses.some(c => c.id === classDetails.id);
  }
}





