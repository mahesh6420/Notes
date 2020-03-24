import { Component, OnInit } from '@angular/core';
import { DataService } from 'src/service/data.service';
import { Note } from 'src/interface/note';

@Component({
  selector : 'note',
  templateUrl: './note.component.html'
})

export class NoteComponent implements OnInit {
  notes: any;
  constructor(private dataService: DataService) {
    dataService.controllerName = 'note';
  }

  ngOnInit(): void {
    this.dataService.getAll().subscribe(res => {console.log(res); return this.notes = res;});

  }
}
