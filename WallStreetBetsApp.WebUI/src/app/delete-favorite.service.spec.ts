import { TestBed } from '@angular/core/testing';

import { DeleteFavoriteService } from './delete-favorite.service';

describe('DeleteFavoriteService', () => {
  let service: DeleteFavoriteService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DeleteFavoriteService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
