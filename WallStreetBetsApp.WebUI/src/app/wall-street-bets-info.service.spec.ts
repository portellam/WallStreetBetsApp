import { TestBed } from '@angular/core/testing';

import { WallStreetBetsInfoService } from './wall-street-bets-info.service';

describe('WallStreetBetsInfoService', () => {
  let service: WallStreetBetsInfoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WallStreetBetsInfoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
