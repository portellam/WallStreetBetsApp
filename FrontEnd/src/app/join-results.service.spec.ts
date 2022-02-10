import { TestBed } from '@angular/core/testing';

import { JoinResultsService } from './join-results.service';

describe('JoinResultsService', () => {
  let service: JoinResultsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(JoinResultsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
