import { Component, ChangeDetectionStrategy, Input } from '@angular/core';
import { AppConsts } from '@shared/AppConsts';
import { AppAuthService } from '@shared/auth/app-auth.service';
import { UtilsService } from 'abp-ng2-module';

@Component({
  selector: 'header-user-menu',
  templateUrl: './header-user-menu.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class HeaderUserMenuComponent {
  @Input() userPicture: string;

  constructor(private _authService: AppAuthService, private _utilsService: UtilsService) { }

  showUpdatePassword(): boolean {
    return true;
  }

  logout(): void {
    this._authService.logout();
  }
}
