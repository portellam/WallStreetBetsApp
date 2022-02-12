import { TestBed } from '@angular/core/testing';

import { AddNoteService } from './add-note.service';

describe('AddNoteService', () => {
  let service: AddNoteService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AddNoteService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
