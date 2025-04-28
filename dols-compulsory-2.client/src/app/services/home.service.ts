import { Injectable, inject } from "@angular/core";
import { Note } from "../models/note.model";
import { firstValueFrom, Observable } from "rxjs";
import { HttpClient, HttpParams } from "@angular/common/http";

@Injectable({
  providedIn: 'root',
})
export class HomeService {
  private http: HttpClient = inject(HttpClient);

  private apiUrl: string = 'http://localhost:80/api/';
  constructor() { }

  getNotes(): Promise<Note[]> {
    const notes: Observable<Note[]> = this.http.get<Note[]>(this.apiUrl + 'Note')
    return firstValueFrom(notes)

  }

  createNote(title: string, content: string) {
    const noteToCreate = {
      Title: title,
      Content: content
    }
    const notes = this.http.post<Note>(this.apiUrl + 'Note', noteToCreate);
    return firstValueFrom(notes);
  }

  deleteNote(id: number): Promise<Note> {
    const deletedNote = this.http.delete<Note>(this.apiUrl + 'Note' + id)
    return firstValueFrom(deletedNote);
  }

  searchNotes(searchValue: string): Promise<Note[]> {
    const params = new HttpParams().set('query', searchValue);
    const filteredNotes = this.http.get<Note[]>(this.apiUrl + 'Note/search', {params});
    return firstValueFrom(filteredNotes);
  }
}
