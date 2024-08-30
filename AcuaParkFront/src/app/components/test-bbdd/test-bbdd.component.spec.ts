import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestBBDDComponent } from './test-bbdd.component';

describe('TestBBDDComponent', () => {
  let component: TestBBDDComponent;
  let fixture: ComponentFixture<TestBBDDComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TestBBDDComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TestBBDDComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
