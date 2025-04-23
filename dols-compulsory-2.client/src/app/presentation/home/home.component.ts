import { Component, inject, Signal } from '@angular/core';
import { HomeFacade } from './home.facade';
import { Note } from '../../models/note.model';

@Component({
  selector: 'app-home',
  standalone: false,
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  private homeFacade: HomeFacade = inject(HomeFacade);
  noteSignal: Signal<Note[]>;
  constructor()
  {
    this.noteSignal = this.homeFacade.getNoteSignal();
  }

  public deleteNote(id: number)
  {
    this.homeFacade.deleteNote(id);
  }
}
