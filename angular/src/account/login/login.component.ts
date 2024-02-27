import { Component, Injector, OnInit } from '@angular/core';
import { AbpSessionService } from 'abp-ng2-module';
import { AppComponentBase } from '@shared/app-component-base';
import { accountModuleAnimation } from '@shared/animations/routerTransition';
import { AppAuthService } from '@shared/auth/app-auth.service';
import { GoogleLoginProvider, SocialAuthService, SocialUser } from '@abacritt/angularx-social-login';

@Component({
  templateUrl: './login.component.html',
  animations: [accountModuleAnimation()]
})
export class LoginComponent extends AppComponentBase implements OnInit{
  submitting = false;

  constructor(
    injector: Injector,
    public authService: AppAuthService,
    private _sessionService: AbpSessionService,
    private socialAuthService: SocialAuthService
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.socialAuthService.authState.subscribe((user : SocialUser) => {
      console.log(user)
      this.externalLogin(
        "ExternalAuth",
        user.id,
        user.idToken
      );
    });
  }

  get multiTenancySideIsTeanant(): boolean {
    return this._sessionService.tenantId > 0;
  }

  get isSelfRegistrationAllowed(): boolean {
    if (!this._sessionService.tenantId) {
      return false;
    }

    return true;
  }

  login(): void {
    this.submitting = true;
    this.authService.authenticate(() => (this.submitting = false));
  }

  externalLogin(
    provider: string,
    providerKey: string,
    providerAccessCode: string
  ) {
    this.authService.externalAuthenticateModel.authProvider = provider;
    this.authService.externalAuthenticateModel.providerKey = providerKey;
    this.authService.externalAuthenticateModel.providerAccessCode =
      providerAccessCode;
    this.authService.externalAuthenticate();
  }
}
