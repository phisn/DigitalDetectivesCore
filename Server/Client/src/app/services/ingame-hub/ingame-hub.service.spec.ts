import { TestBed } from '@angular/core/testing';

import { IngameHubService } from './ingame-hub.service';

describe('IngameHubService', () => {
  let service: IngameHubService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(IngameHubService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
