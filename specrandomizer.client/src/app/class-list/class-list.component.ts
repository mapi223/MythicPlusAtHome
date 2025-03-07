import { Component, EventEmitter, Input, Output, SimpleChanges } from '@angular/core';
import { IClassDetails } from './classDetails';
import { CLASSLIST } from './mock-list';


@Component({
  selector: 'app-class-list',
  templateUrl: './class-list.component.html',
  styleUrls: ['./class-list.component.css']
})

export class ClassListComponent {
  @Input() isLoading = false;
  classes = CLASSLIST;
  selectedClass?: IClassDetails;
  selectedClasses: IClassDetails[] = [];
  activeIndex: number | null = null;
  private cycleInterval: any;

  @Output() selectionOutput = new EventEmitter<IClassDetails[]>();

  ngOnChanges(changes: SimpleChanges) {
    if (changes['isLoading']) {
      if (this.isLoading) {
        this.startSlotMachineEffect();
      } else {
        this.stopSlotMachineEffect();
      }
    }
  }

  onSelect(classDetails: IClassDetails): void {
    this.selectedClass = classDetails;
    if (this.selectedClasses?.indexOf(classDetails) !== undefined) {
      const foundClass: number = this.selectedClasses.indexOf(classDetails)
      if (foundClass === -1) {
        this.selectedClasses.push(classDetails);
        this.selectionOutput.emit(this.selectedClasses);
      }
      else {
        this.selectedClasses.splice(foundClass, 1);
        this.selectionOutput.emit(this.selectedClasses);
      }
    }
  }
  isSelectedArray(classDetails: IClassDetails) {
    if (this.selectedClasses?.indexOf(classDetails) !== undefined) {
      if (this.selectedClasses?.indexOf(classDetails) >= 0) {
        return true;
      }
      else
        return false;
    }
    else
      return false;
  }

  startSlotMachineEffect() {
    let index = 0;
    let id = 0;
    this.cycleInterval = setInterval(() => {
      if (!this.isLoading || this.selectedClasses?.length === 0) {
        this.stopSlotMachineEffect();
        return;
      }
      this.activeIndex = id;
      index = (index + 1) % this.selectedClasses?.length;
      id = this.selectedClasses[index].id;
    }, 500);
  }

  stopSlotMachineEffect() {
    clearInterval(this.cycleInterval);
    this.activeIndex = null;
  }
}

