import { TestBed } from '@angular/core/testing';

import { GameHubServiceService } from './game-hub-service.service';

describe('GameHubServiceService', () => {
  let service: GameHubServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GameHubServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
