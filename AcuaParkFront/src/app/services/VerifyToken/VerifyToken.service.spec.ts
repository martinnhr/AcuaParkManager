import { TestBed } from '@angular/core/testing';

import { VerifyToken } from './VerifyToken.service';

describe('VerifyToken', () => {
  let service: VerifyToken;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(VerifyToken);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
