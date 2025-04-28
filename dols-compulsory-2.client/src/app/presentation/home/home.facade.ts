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
    this.loadNotes();
    return this.noteSignal;
  }

  async createNote(title: string, content: string) {
    const createdNote = await this.homeManager.createNote(title, content);
    this.noteSignal.update((currentNotes) => [...currentNotes, createdNote]);
  }


  deleteNote(id: number) {
    this.homeManager.deleteNote(id);
  }

  async searchNotes(searchValue: string) {
    const filteredNotes = await this.homeManager.searchNotes(searchValue);
    this.noteSignal.set(filteredNotes);
  }
}
