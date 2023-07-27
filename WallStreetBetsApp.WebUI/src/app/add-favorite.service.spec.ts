import { TestBed } from '@angular/core/testing';

import { AddFavoriteService } from './add-favorite.service';

describe('AddFavoriteService', () => {
  let service: AddFavoriteService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AddFavoriteService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
