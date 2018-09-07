import { Component, OnInit, Input, OnChanges, ViewChild, ElementRef, SimpleChanges, AfterViewInit } from '@angular/core';

@Component({
  selector: 'bl-video-background',
  templateUrl: './video-background.component.html',
  styleUrls: ['./video-background.component..scss']
})
export class VideoBackgroundComponent implements OnInit, OnChanges, AfterViewInit {
  @Input() 
  videoLocation : string;

  @ViewChild('videoElement')
  videoEle : ElementRef;

  constructor() { }

  ngOnInit() {    
  }

  ngOnChanges(changes : SimpleChanges)
  {
  }

  ngAfterViewInit()
  {
    
    const player = this.videoEle.nativeElement;
    //Chrome 66+ enforces a video element to have the 'muted' attribute, or it won't allow autoplay
    player.muted = true;
    player.load();
  }



}
