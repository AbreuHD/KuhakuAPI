// En lugar de usar IHttpContextAccessor, solo inicializas TestingFlareSolverr sin él
var webScraping = new TestingFlareSolverr();
await webScraping.GetFromHDFull();
