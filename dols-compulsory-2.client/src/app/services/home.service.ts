import { Injectable, inject } from "@angular/core";
import { Note } from "../models/note.model";
import { catchError, firstValueFrom, map, Observable, of } from "rxjs";
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

  createNote(title: string, content: string): Promise<Note> {
    const noteToCreate = {
      Title: title,
      Content: content
    };

    return firstValueFrom(this.http.post<Note>(this.apiUrl + 'Note', noteToCreate))
      .then((createdNote: Note) => {
        return createdNote;
      })
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

  
  getFeatureFlag(featureflag: string): Promise<boolean> {
    const params = new HttpParams().set('featureName', featureflag);
    return firstValueFrom(
      this.http.get<FeatureFlagResponse>(this.apiUrl + 'Note/feature-flag', { params }).pipe(
        map((response) => response.isEnabled),
        catchError((error) => {
          console.error('Fejl ved hentning af feature flag:', error);
          return of(false);
        })
      )
    );
  }

  
}

interface FeatureFlagResponse {
  featureName: string;
  isEnabled: boolean;
}