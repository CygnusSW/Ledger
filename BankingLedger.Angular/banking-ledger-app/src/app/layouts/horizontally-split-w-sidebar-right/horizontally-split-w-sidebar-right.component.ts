import { Component, OnInit } from '@angular/core';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'bl-horizontally-split-w-sidebar-right',
  templateUrl: './horizontally-split-w-sidebar-right.component.html',
  styleUrls: ['./horizontally-split-w-sidebar-right.component..scss']
})
export class HorizontallySplitWSidebarRightComponent implements OnInit {
  //Credit: https://www.shutterstock.com/video/clip-2540876?irgwc=1&utm_medium=Affiliate&utm_campaign=Oxford%20Media%20Solutions&utm_source=51471&utm_term=
  public videoLocation = "/assets/videos/scrolling_stock_footage.webm";//Would purchase license, etc for a production system

  constructor() { }

  ngOnInit() {
  }

}
