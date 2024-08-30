import { TestBed } from '@angular/core/testing';

import { TestBBDDServiceService } from './test-bbddservice.service';

describe('TestBBDDServiceService', () => {
  let service: TestBBDDServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TestBBDDServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
