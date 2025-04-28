import { Component, inject, signal, Signal, WritableSignal } from '@angular/core';
import { HomeFacade } from './home.facade';
import { Note } from '../../models/note.model';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  private homeFacade: HomeFacade = inject(HomeFacade);
  noteSignal: Signal<Note[]>;
  noteSearchValue: WritableSignal<string>
  noteCreationReady: boolean = false;
  noteTitleValue: WritableSignal<string>;
  noteContentValue: WritableSignal<string>;
  constructor()
  {
    this.noteSignal = this.homeFacade.getNoteSignal();
    console.log("Test"+this.noteSignal());
    this.noteSearchValue = signal('');
    this.noteTitleValue = signal('');
    this.noteContentValue = signal('');
  }

  createNote() {
    this.homeFacade.createNote(this.noteTitleValue(), this.noteContentValue());
  }

  public deleteNote(id: number)
  {
    this.homeFacade.deleteNote(id);
  }

  searchNote() {
    const searchValue = this.noteSearchValue();
    this.homeFacade.searchNotes(searchValue);
  }
  readyCreateNote() {
    this.noteCreationReady = true;
  }
  cancelCreateNote() {
    this.noteCreationReady = false;
  }
}
