import { Component, Injector, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/app-component-base';
import { CameraDto, CameraServiceProxy, DetectionEventDto } from '@shared/service-proxies/service-proxies';

@Component({
  templateUrl: './camera-detail.component.html',
  animations: [appModuleAnimation()],
  styleUrls: ['./camera-detail.component.scss']
})
export class CameraDetailComponent extends AppComponentBase implements OnInit {

  cameraId: number;
  camera: CameraDto;
  detectionEvents: DetectionEventDto[] = [];

  responsiveOptions: any[] | undefined;

  constructor(private route: ActivatedRoute, injector: Injector, private _cameraService: CameraServiceProxy) {
    super(injector);
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.cameraId = +params['id'];
      this.getCamera();
    });

    this.responsiveOptions = [
      {
        breakpoint: '1199px',
        numVisible: 1,
        numScroll: 1
      },
      {
        breakpoint: '991px',
        numVisible: 2,
        numScroll: 1
      },
      {
        breakpoint: '767px',
        numVisible: 1,
        numScroll: 1
      }
    ];
  }

  getCamera() {
    this._cameraService.get(this.cameraId).subscribe((result) => {
      this.camera = result;
      this.detectionEvents = this.camera.detectionEvents;
      console.log(this.camera);
    });
  }

  getSeverityCameraState(state: number) {
    switch (state) {
      case 0:
        return 'warning';
      case 1:
        return 'success';
    }
  }

  getSeverityEntityType(state: number) {
    switch (state) {
      case 0:
        return 'success';
      case 1:
        return 'warning';
      case 2:
        return 'danger';
    }
  }

  getIconEntityType(state: number): string {
    switch (state) {
      case 0:
        return 'Normal';
      case 1:
        return 'Avertissement';
      case 2:
        return 'Menace';
    }
  }

  formatMomentDate(detectedTime: moment.Moment | undefined): string {
    if (!detectedTime) return ''; // Vérifie si la date est définie

    return detectedTime.format('DD/MM/YYYY HH:mm:ss');
  }

  deleteDetectionEvent(detectionEvent: DetectionEventDto) {

  }

}
