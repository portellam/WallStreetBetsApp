import { TestBed } from '@angular/core/testing';

import { GetNotesService } from './get-notes.service';

describe('GetNotesService', () => {
  let service: GetNotesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GetNotesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
