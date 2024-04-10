import { Time } from '@angular/common';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';

@Component({
  selector: 'app-exam',
  templateUrl: './exam.component.html',
  styleUrl: './exam.component.css'
})
export class ExamComponent implements OnInit {
  timeString: string = "Time : 00:00:00"; 
  timerInterval: any;
   localStream:any  = null;
  chunks = [];
  containerType = "video/webm";
  mediaRecorder: any;
  options: any;
  soundMeter: any = null;
  @ViewChild('videoElement') videoElement!: ElementRef;
  liveVideoElement:any = document.querySelector('#gum'); 
   constraints = { audio: true, video: { width: { min: 640, ideal: 640, max: 640 }, height: { min: 480, ideal: 480, max: 480 }, framerate: 60 } };
    ngOnInit(): void {
      if (!navigator.mediaDevices.getUserMedia) {
        alert('navigator.mediaDevices.getUserMedia not supported on your browser, use the latest version of Firefox or Chrome');
      } else {
        if (window.MediaRecorder == undefined) {
          alert('MediaRecorder not supported on your browser, use the latest version of Firefox or Chrome');
        } else {
          navigator.mediaDevices.getUserMedia(this.constraints)
            .then( (stream)=> {
              this.localStream = stream;

              this.localStream.getTracks().forEach((track: MediaStreamTrack)=> {
                if (track.kind == "audio") {
                  track.onended =  (event:Event)=> {
                    console.log("audio track.onended Audio track.readyState=" + track.readyState + ", track.muted=" + track.muted);
                  }
                }
                if (track.kind == "video") {
                  track.onended =  (event:Event)=> {
                    console.log("video track.onended Audio track.readyState=" + track.readyState + ", track.muted=" + track.muted);
                  }
                }
              });

              /*this.liveVideoElement.srcObject = this.localStream;
              this.liveVideoElement.play();*/
              this.videoElement.nativeElement.srcObject = this.localStream;
              this.videoElement.nativeElement.play();

              //try {
              //  window.AudioContext = window.AudioContext || window.webkitAudioContext;
              //  window.audioContext = new AudioContext();
              //} catch (e) {
              //  console.log('Web Audio API not supported.');
              //}

              //this.soundMeter = window.soundMeter = new SoundMeter(window.audioContext);
              //this.soundMeter.connectToSource(this.localStream,e=> {
              //  if (e) {
              //    console.log(e);
              //    return;
              //  } else {
              //    /*setInterval(function() {
              //       log(Math.round(soundMeter.instant.toFixed(2) * 100));
              //   }, 100);*/
              //  }
              //});

            }).catch(function (err) {
              /* handle the error */
              console.log('navigator.getUserMedia error: ' + err);
            });
        }
      }
    }



  startExam() {

    this.StartRecord();
    this.StartTimer(1, 0);
  }
   StartTimer(Duration:number,checkTime:any) {
  var deadline = new Date();
  deadline.setHours(deadline.getHours() + Duration);
  if (true) {
    var x = setInterval( ()=> {
      var now = new Date().getTime();
      var t = deadline.getTime() - now;
      var hours = Math.floor((t % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
      var minutes = Math.floor((t % (1000 * 60 * 60)) / (1000 * 60));
      var seconds = Math.floor((t % (1000 * 60)) / 1000);
      this.updateTimer(hours, minutes, seconds);
    }, 1000);
    checkTime.push(x);
  }
  }
  updateTimer(hours: number, minutes: number, seconds: number) {
    this.timeString = `Time: ${hours < 10 ? '0' + hours : hours}:${minutes < 10 ? '0' + minutes : minutes}:${seconds < 10 ? '0' + seconds : seconds}`;
    if (hours === 0 && minutes === 0 && seconds === 0) {
      clearInterval(this.timerInterval);
      this.timeString = "Time : 00:00:00";
    }
  }

   StartRecord() {
  if (this.localStream == null) {
    alert('Could not get local stream from mic/camera');
  } else {
    this.chunks = [];
    /* use the stream */
    console.log('Start recording...');
    if (typeof MediaRecorder.isTypeSupported == 'function') {
      /*
          MediaRecorder.isTypeSupported is a function announced in https://developers.google.com/web/updates/2016/01/mediarecorder and later introduced in the MediaRecorder API spec http://www.w3.org/TR/mediastream-recording/
      */
       ;
      if (MediaRecorder.isTypeSupported('video/webm;codecs=vp9')) {
         this.options = { mimeType: 'video/webm;codecs=vp9' };
      } else if (MediaRecorder.isTypeSupported('video/webm;codecs=h264')) {
        this.options = { mimeType: 'video/webm;codecs=h264' };
      } else if (MediaRecorder.isTypeSupported('video/webm')) {
        this.options = { mimeType: 'video/webm' };
      } else if (MediaRecorder.isTypeSupported('video/mp4')) {
        //Safari 14.0.2 has an EXPERIMENTAL version of MediaRecorder enabled by default
        this.containerType = "video/mp4";
        this.options = { mimeType: 'video/mp4' };
      }
      //console.log('Using ' + options.mimeType);
      this.mediaRecorder = new MediaRecorder(this.localStream, this.options);
    } else {
      console.log('isTypeSupported is not supported, using default codecs for browser');
      this.mediaRecorder = new MediaRecorder(this.localStream);
    }

    this.mediaRecorder.ondataavailable = function (e:any) {
      console.log('mediaRecorder.ondataavailable, e.data.size=' + e.data.size);
      if (e.data && e.data.size > 0) {
        this.chunks.push(e.data);
      }
    };

    this.mediaRecorder.onerror = function (e:any) {
      console.log('mediaRecorder.onerror: ' + e);
    };

    this.mediaRecorder.onstart = function () {
      console.log('mediaRecorder.onstart, mediaRecorder.state = ' + this.mediaRecorder.state);

      this.localStream.getTracks().forEach(function (track:any) {
        if (track.kind == "audio") {
          console.log("onstart - Audio track.readyState=" + track.readyState + ", track.muted=" + track.muted);
        }
        if (track.kind == "video") {
          console.log("onstart - Video track.readyState=" + track.readyState + ", track.muted=" + track.muted);
        }
      });
    };

    this.mediaRecorder.onstop = function () {
      console.log('mediaRecorder.onstop, mediaRecorder.state = ' + this.mediaRecorder.state);

      //var recording = new Blob(chunks, {type: containerType});
      var recording = new Blob(this.hunks, { type: this.mediaRecorder.mimeType });
      //PostBlob(recording);
    };

    this.mediaRecorder.onpause = function () {
      console.log('mediaRecorder.onpause, mediaRecorder.state = ' + this.mediaRecorder.state);
    }

    this.mediaRecorder.onresume = function () {
      console.log('mediaRecorder.onresume, mediaRecorder.state = ' + this.mediaRecorder.state);
    }

    this.mediaRecorder.onwarning = function (e:any) {
      console.log('mediaRecorder.onwarning: ' + e);
    };

    this.mediaRecorder.start(1000);

    this.localStream.getTracks().forEach(function (track:any) {
      console.log(track.kind + ":" + JSON.stringify(track.getSettings()));
      console.log(track.getSettings());
    })
  }
}
}
