import { Component } from '@angular/core';
import { TestBBDDServiceService } from '../../services/test-bbddservice.service';

@Component({
  selector: 'app-test-bbdd',
  standalone: true,
  imports: [],
  templateUrl: './test-bbdd.component.html',
  styleUrl: './test-bbdd.component.scss'
})
export class TestBBDDComponent {
  

  res: string = "";

  constructor(private httpService: TestBBDDServiceService) {}

  ngOnInit(){

    this.httpService.getTestBBDD().subscribe((data: string) => {
      this.res = data;

    })
  }
}
