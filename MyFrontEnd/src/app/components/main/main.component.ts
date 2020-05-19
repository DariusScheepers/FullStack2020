import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { DataService } from 'src/app/services/data/data.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit {

  data = `No Data Provided.`;
  testOutput = "Nothing";

  newCarFromGroup: FormGroup;
  testConnectionFormGroup: FormGroup;
  constructor(private dataService: DataService) { }

  ngOnInit(): void {
    this.getData();

    this.newCarFromGroup = new FormGroup({
      name: new FormControl(''),
      price: new FormControl('')
    });
    this.testConnectionFormGroup = new FormGroup({
      url: new FormControl('')
    });
  }

  getData() {
    const api = "/1";
    this.dataService.getData(api).subscribe((data) => {
      console.log('Info: ', data);      
      this.data = JSON.stringify(data);
    }, (error) => {
      console.error(`get request to ${api}`, error);
    });
  }

  submit() {
    let car = {
      name: this.newCarFromGroup.controls.name.value,
      price: this.newCarFromGroup.controls.price.value,
    };
    console.log('Info: ', car);
    
    this.dataService.postData('/addcar', car).subscribe(data => {
      console.log('Post successful: ', data);
    }, error => {
      console.error('Post error: ', error);
    });
    // this.newCarFromGroup.controls.name.setValue('');
    // this.newCarFromGroup.controls.price.setValue(0);
  }

  testConnection() {
    this.dataService.testConnection(this.testConnectionFormGroup.controls.url.value).subscribe(data => {
      this.testOutput = JSON.stringify(data)
    }, error => {
      console.error('Info Connection: ', error);      
    });
  }
}
