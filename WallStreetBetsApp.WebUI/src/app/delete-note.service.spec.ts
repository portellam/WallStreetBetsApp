import { TestBed } from '@angular/core/testing';

import { DeleteNoteService } from './delete-note.service';

describe('DeleteNoteService', () => {
  let service: DeleteNoteService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DeleteNoteService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
