import { ProjetoApolloTemplatePage } from './app.po';

describe('ProjetoApollo App', function() {
  let page: ProjetoApolloTemplatePage;

  beforeEach(() => {
    page = new ProjetoApolloTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
