import { Component, OnInit } from '@angular/core';
import { DataService } from 'src/service/data.service';
import { Note } from 'src/interface/note';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { ResultStatus } from 'src/common/enum/result-status';
import { QueryParam } from 'src/interface/query-param';

@Component({
  selector : 'note',
  templateUrl: './note.component.html'
})

export class NoteComponent implements OnInit {
  notes: any;
  selectedNote: Note;
  updateNote: boolean = false;
  queryParam: QueryParam = {
    searchText: '',
    pageNo: 1,
    take: 10
  }
  totalPageNo: number[] = [1];
  totalCount: number;

  showResult: FormControl = new FormControl(this.queryParam.take);
  searchText: FormControl = new FormControl('');

  constructor(private _dataService: DataService<Note>) {
    _dataService.controllerName = 'note';
  }

  ngOnInit(): void {
    this._dataService.getAll(this.queryParam)
    .subscribe(res => this.notes = res);

    this.populatePageNo();
  }

  paginate(pageNo: number, take?: number, searchText?: string) {
    this.queryParam.pageNo = pageNo;
    this.queryParam.take = take || this.queryParam.take;
    this.queryParam.searchText = searchText || this.queryParam.searchText;

    this._dataService.getAll(this.queryParam)
    .subscribe(res => {
      this.notes = res;
    });
  }

  addNote(note: Note) {
    this.notes.splice(0, 0, note);
    this.populatePageNo();
  }

  editNote(note: Note) {
    this.selectedNote = note;
  }

  deleteNote(note: Note) {
    this._dataService.delete(note).subscribe((res) => {
      if(res.status === ResultStatus.Success) {

        const index = this.notes.indexOf(note);
        this.notes.splice(index, 1);
      }
    });
  }

  populatePageNo() {
    this._dataService.getTotalCount(this.queryParam.take)
    .subscribe(res => {
      this.totalCount = res.totalCount;
      let i: number;
      for (i = 1; i < res.totalPage; i++) {
        this.totalPageNo[i] = i + 1;
    }});

    return;
  }

  removeSelectedNote(event) {
    this.selectedNote = null;
  }
  updateUpdatedNote(note: Note) {
    const selectedNote = this.notes.indexOf(this.selectedNote);
    this.notes.splice(selectedNote, 1, note);
  }
}
