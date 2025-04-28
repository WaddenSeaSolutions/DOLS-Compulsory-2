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
  isFlagEnabled: boolean = true;

  constructor()
  {
    this.noteSignal = this.homeFacade.getNoteSignal();
    this.noteSearchValue = signal('');
    this.noteTitleValue = signal('');
    this.noteContentValue = signal('');

  }

  createNote() {
    this.homeFacade.createNote(this.noteTitleValue(), this.noteContentValue());
     this.updateNoteSignal();
  }

   updateNoteSignal() {
     this.noteSignal = this.homeFacade.getNoteSignal();
   }

  public deleteNote(id: number)
  {
    this.homeFacade.deleteNote(id);
  }

  async searchNote() {
    this.isFlagEnabled = await this.homeFacade.getFeatureFlag("search");
    
    if(this.isFlagEnabled){
      const searchValue = this.noteSearchValue();
      this.homeFacade.searchNotes(searchValue);
    }
    else {
      alert("Search is disabled")
    }
  }

  readyCreateNote() {
    this.noteCreationReady = true;
  }
  cancelCreateNote() {
    this.noteCreationReady = false;
  }
}
