class Configuration {
  private BACKEND_BASE_URL: string = "https://localhost:44339/api/";

  getURL(endpoint: string): string {
    return this.BACKEND_BASE_URL.concat(endpoint);
  }
}

export const Config = new Configuration();