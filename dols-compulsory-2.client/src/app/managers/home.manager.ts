import { inject, Injectable, signal, Signal, WritableSignal } from "@angular/core";
import { Note } from "../models/note.model";
import { HomeService } from "../services/home.service";

@Injectable({
  providedIn: 'root',
})
export class HomeManager {

  private homeService: HomeService = inject(HomeService);
  private noteSignal: WritableSignal<Note[]>;
  constructor() {
    this.noteSignal = signal([]);
  }

  async getNotes(): Promise<Note[]> {
   return await this.homeService.getNotes();
  }

  createNote(title: string, content: string) {
    return this.homeService.createNote(title, content);
  }

  deleteNote(id: number) {
    this.homeService.deleteNote(id);
  }

  async searchNotes(searchValue: string){
    return this.homeService.searchNotes(searchValue);

  }

  async getFeatureFlag(featureflag: string): Promise<boolean> {
    return this.homeService.getFeatureFlag(featureflag);
  }
}
