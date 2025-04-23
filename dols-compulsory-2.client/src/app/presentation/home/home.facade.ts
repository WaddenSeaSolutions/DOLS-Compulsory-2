import { computed, inject, Injectable, signal, Signal } from "@angular/core";
import { Note } from "../../models/note.model";
import { HomeManager } from "../../managers/home.manager";

@Injectable({
  providedIn: 'root',
})
export class HomeFacade {
  private homeManager: HomeManager = inject(HomeManager);

  noteSignal = signal<Note[]>([]);

  constructor() {
    this.loadNotes();
  }

  async loadNotes() {
    const notes = await this.getNotes();
    this.noteSignal.set(notes);
  }

  async getNotes(): Promise<Note[]> {
    return this.homeManager.getNotes();
  }

  getNoteSignal() {
    return this.noteSignal;
  }

  deleteNote(id: number) {
    this.homeManager.deleteNote(id);
  }
}
