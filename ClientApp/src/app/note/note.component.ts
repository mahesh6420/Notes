import { Component, OnInit } from '@angular/core';
import { DataService } from 'src/service/data.service';
import { Note } from 'src/interface/note';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { ResultStatus } from 'src/common/enum/result-status';

@Component({
  selector : 'note',
  templateUrl: './note.component.html'
})

export class NoteComponent implements OnInit {
  notes: any;

  constructor(private _dataService: DataService<Note>) {
    _dataService.controllerName = 'note';
  }

  ngOnInit(): void {
    this._dataService.getAll()
    .subscribe(res => this.notes = res);
  }

  addNote(note: Note) {
    this.notes.splice(0, 0, note);
  }

  editNote(note: Note) {
  }

  deleteNote(note: Note) {
    this._dataService.delete(note).subscribe((res) => {
      if(res.status === ResultStatus.Success) {
        const index = this.notes.indexOf(note);
        this.notes.splice(index, 1);
      }
    });
  }
}
