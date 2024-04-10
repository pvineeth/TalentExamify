import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminnavmenuComponent } from './adminnavmenu.component';

describe('AdminnavmenuComponent', () => {
  let component: AdminnavmenuComponent;
  let fixture: ComponentFixture<AdminnavmenuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AdminnavmenuComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AdminnavmenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
