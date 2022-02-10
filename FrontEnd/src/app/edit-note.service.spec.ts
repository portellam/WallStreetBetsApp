import { TestBed } from '@angular/core/testing';

import { EditNoteService } from './edit-note.service';

describe('EditNoteService', () => {
  let service: EditNoteService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EditNoteService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
