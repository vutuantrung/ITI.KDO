class AuthService {
  constructor() {
    this.fakeIdentity = '';
    this.logoutEndpoint = null;
    this.providers = {};
    this.allowedOrigins = [];
    this.authenticatedCallbacks = [];
    this.signedOutCallbacks = [];
  }

  get identity() {
    return this.fakeIdentity;
    // return ITI.KDO.getIdentity();
  }

  set identity(i) {
    this.fakeIdentity = i;
    // ITI.KDO.setIdentity(i);
  }

  get isConnected() {
    return this.identity != null;
  }

  get accessToken() {
    var identity = this.identity;
    return identity ? identity.bearer.access_token : null;
  }

  get email() {
    var identity = this.identity;
    return identity ? identity.email : null;
  }

  get boundProviders() {
    var identity = this.identity;
    return identity ? identity.boundProviders : [];
  }

  isBoundToProvider = expectedProviders => {
    let isBound = false;
    for (let p of expectedProviders) {
      if (this.boundProviders.indexOf(p) > -1) isBound = true;
    }

    return isBound;
  };

  onAuthenticated(i) {
    this.identity = i;
    for (let cb of this.authenticatedCallbacks) {
      cb();
    }
  }

  onSignedOut() {
    this.identity = null;
    for (let cb of this.signedOutCallbacks) {
      cb();
    }
  }

  onMessage(e) {
    if (this.allowedOrigins.indexOf(e.origin) < 0) return;

    let data = typeof e.data === 'string' ? JSON.parse(e.data) : e.data;

    if (data.type === 'authenticated') this.onAuthenticated(data.payload);
    if (data.type === 'signedOut') this.onSignedOut();
  }

  login(selectedProvider) {
    const provider = this.providers[selectedProvider];
    window.open(
      provider.endpoint,
      'Connexion à ITI.PrimarySchool',
      'menubar=no, status=no, scrollbars=no, menubar=no, width=700, height=700'
    );
  }

  logout() {
    window.open(
      this.logoutEndpoint,
      "Déconnexion d'ITI.PrimarySchool",
      'menubar=no, status=no, scrollbars=no, menubar=no, width=700, height=600'
    );
  }

  registerSignedOutCallback(cb) {
    this.signedOutCallbacks.push(cb);
  }

  removeSignedOutCallback(cb) {
    this.signedOutCallbacks.splice(this.signedOutCallbacks.indexOf(cb), 1);
  }
}

export default AuthService;
