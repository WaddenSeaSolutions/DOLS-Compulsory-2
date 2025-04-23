import { Component, inject, signal, Signal, WritableSignal } from '@angular/core';
import { HomeFacade } from './home.facade';
import { Note } from '../../models/note.model';
import { FormControl, FormGroup, FormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  private homeFacade: HomeFacade = inject(HomeFacade);
  noteSignal: Signal<Note[]>;
  noteSearchValue:WritableSignal<string>
  constructor()
  {
    this.noteSignal = this.homeFacade.getNoteSignal();
    this.noteSearchValue = signal('');
  }

  createNote(title: string, content: string) {
    this.homeFacade.createNote(title, content);
  }

  public deleteNote(id: number)
  {
    this.homeFacade.deleteNote(id);
  }

  searchNote() {
    const searchValue = this.noteSearchValue();
    const filteredNotes = this.noteSignal().filter(note => note.Title.toLowerCase().includes(searchValue.toLowerCase()));
    return filteredNotes;
  }
}
