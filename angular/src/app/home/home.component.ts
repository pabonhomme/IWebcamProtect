import { Component, Injector, ChangeDetectionStrategy, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { CameraDto, CameraServiceProxy } from '@shared/service-proxies/service-proxies';
import { forEach } from 'lodash';

@Component({
  templateUrl: './home.component.html',
  animations: [appModuleAnimation()],
  styleUrls: ["./home.component.scss"],
})
export class HomeComponent extends AppComponentBase implements OnInit {
  cameraDialog: boolean = false;

  camera!: CameraDto;
  submitted: boolean = false;

  cameras: CameraDto[];

  selectedCameras!: CameraDto[] | null;

  stateOptions: any[] = [{ label: 'Off', value: 0 }, { label: 'On', value: 1 }];

  constructor(injector: Injector, private _cameraService: CameraServiceProxy) {
    super(injector);
  }
  ngOnInit(): void {
    this.getCamerasByUser();
  }

  getCamerasByUser() {
    this._cameraService.getAllByUser(undefined)
      .subscribe((result) => {
        console.log(result.items)
        this.cameras = result.items;
      });
  }

  getSeverity(state: number) {
    switch (state) {
      case 1:
        return 'success';
      case 2:
        return 'warning';
    }
  }

  openNew() {
    this.camera = new CameraDto();
    this.submitted = false;
    this.cameraDialog = true;
    console.log(this.camera.id)
  }

  editCamera(camera: CameraDto) {
    this.camera = new CameraDto(camera);
    console.log(this.camera.id)
    this.cameraDialog = true;
  }

  deleteSelectedCameras() {
    abp.message.confirm(
      this.l("DeleteCamerasConfirm"),
      undefined,
      (result: boolean) => {
        if (result) {
          this.selectedCameras.forEach(async (camera) => this.deleteCamera(camera.id))
        }
      }
    );
  }



  deleteOneCamera(camera: CameraDto) {
    abp.message.confirm(
      this.l("DeleteCameraConfirm"),
      undefined,
      (result: boolean) => {
        if (result) {
          this.deleteCamera(camera.id);
        }
      }
    );
  }

  deleteCamera(id: number) {
    this._cameraService.delete(id).subscribe(
      () => {
        this.notify.info(this.l("DeletedSuccessfully"));
        this.getCamerasByUser();
      },
      () => {
      }
    );
  }

  hideDialog() {
    this.cameraDialog = false;
    this.submitted = false;
  }

  saveCamera() {
    this.submitted = true;

    if (this.camera.name?.trim()) {
      if (this.camera.id !== undefined) {
        this._cameraService.update(this.camera.name, this.camera.state, this.appSession.userId, this.camera.id).subscribe(() => {
          this.getCamerasByUser();
          this.notify.success(this.l("EditedSucessfully"));
        });
      } else {
        this._cameraService.create(this.camera.reference, this.camera.name, this.camera.state, this.appSession.userId).subscribe(() => {
          this.getCamerasByUser();
          this.notify.success(this.l("AddedSucessfully"));
        });
      }

      this.cameras = [...this.cameras];
      this.cameraDialog = false;
      this.camera = new CameraDto();
    }
  }
}
