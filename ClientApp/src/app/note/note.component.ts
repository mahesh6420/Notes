import { Component, OnInit } from '@angular/core';
import { DataService } from 'src/service/data.service';
import { Note } from 'src/interface/note';
import { FormGroup, Validators, FormControl } from '@angular/forms';

@Component({
  selector : 'note',
  templateUrl: './note.component.html'
})

export class NoteComponent implements OnInit {
  notes: any;
  form = new FormGroup({
    id: new FormControl(''),
    title: new FormControl('', Validators.required),
    description: new FormControl('', Validators.required)
  });

  constructor(private _dataService: DataService<Note>) {
    _dataService.controllerName = 'note';
  }

  ngOnInit(): void {
    this._dataService.getAll().subscribe(res => {console.log(res); return this.notes = res;});
  }

  createNote(note: Note) {
    this._dataService.create(note);
  }

  editNote(note: Note) {
  }

  deleteNote(note: Note) {

  }
}
