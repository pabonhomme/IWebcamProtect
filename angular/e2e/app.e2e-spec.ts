import { IWebcamProtectTemplatePage } from './app.po';

describe('IWebcamProtect App', function() {
  let page: IWebcamProtectTemplatePage;

  beforeEach(() => {
    page = new IWebcamProtectTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
