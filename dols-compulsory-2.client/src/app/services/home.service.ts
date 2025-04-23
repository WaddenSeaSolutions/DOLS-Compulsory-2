import { Injectable, inject } from "@angular/core";
import { Note } from "../models/note.model";
import { firstValueFrom, Observable } from "rxjs";
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: 'root',
})
export class HomeService {
  private http: HttpClient = inject(HttpClient);

  private apiUrl: string = 'https://localhost:44310/api/';
  constructor() { }

  getNotes(): Promise<Note[]> {
    const notes: Observable<Note[]> = this.http.get<Note[]>(this.apiUrl + 'Notes/GetNotes')
    return firstValueFrom(notes)

  }
}
