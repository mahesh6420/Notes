import { Component, Output, EventEmitter, Input, OnInit, OnChanges } from '@angular/core';
import { DataService } from 'src/service/data.service';
import { Note } from 'src/interface/note';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { DataResult } from 'src/interface/data-result';
import { ResultStatus } from 'src/common/enum/result-status';

@Component({
  selector : 'note-form',
  templateUrl: './note-form.component.html'
})

export class NoteFormComponent implements OnChanges {
  @Output() note: EventEmitter<Note> = new EventEmitter<Note>();
  @Output() removeSelectedNote: EventEmitter<Note> = new EventEmitter<Note>();
  @Output() updateUpdatedNote: EventEmitter<Note> = new EventEmitter<Note>();
  @Input() editNote: Note = null;
  form = new FormGroup({
    id: new FormControl(0),
    title: new FormControl('', Validators.required),
    description: new FormControl('', Validators.required)
  });

  constructor(private _dateService: DataService<Note>) {
    _dateService.controllerName = 'note';
  }

  set UpdateNote(note: Note) {
    this.editNote = note;
    this.form.patchValue(note);
  }

  ngOnChanges() {
    this.form.patchValue(this.editNote || {});
  }

  addNote() {
    if (this.form.valid) {
      const formdata: Note = Object.assign({}, this.form.value);

      if(formdata.id > 0) {
        this.updateNote(formdata);
        return;
      }
      this._dateService.create(formdata).subscribe((res) => {
        if (res.status === ResultStatus.Success) {
          this.note.emit(res.data);
          this.cancelUpdate();
          return;
        }
        //TODO: Toaster here - error toaster
      });
    }
  }

  updateNote(note: Note) {
    this._dateService.update(note).subscribe((res) => {
      if (res.status === ResultStatus.Success) {
        this.updateUpdatedNote.emit(res.data);
        this.cancelUpdate();
        return;
      }
      //TODO: Toaster here - error toaster
    });
  }

  cancelUpdate() {
    this.form.reset();
    this.form.patchValue({id: 0});
    this.editNote = null;
    this.removeSelectedNote.emit();
  }
}
