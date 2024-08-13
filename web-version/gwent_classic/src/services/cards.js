

class DriveService {
    constructor() {
    this.isSignedIn();

    }
    async isSignedIn() {
        this.isAuthorized = await this.$google.api.auth2.getAuthInstance().isSignedIn.get();
        console.log(this.isAuthorized);
        this.userInfo = await this.$google.api.auth2.getAuthInstance().currentUser.get();
      }

}

export default new DriveService();