import { Component, OnInit } from '@angular/core';
import { DataService } from 'src/service/data.service';
import { Note } from 'src/interface/note';
import { FormGroup, Validators, FormControl } from '@angular/forms';

@Component({
  selector : 'note-form',
  templateUrl: './note-form.component.html'
})

export class NoteFormComponent {
  notes: any;
  form = new FormGroup({
    id: new FormControl(''),
    title: new FormControl('', Validators.required),
    description: new FormControl('', Validators.required)
  });

  constructor(private _dateService: DataService<Note>) {
  }

  addNote() {
    if(this.form.valid) {
      const formdata: Note = Object.assign({}, this.form.value);
      console.log(formdata);
    }
  }
}
