export interface LoginResponseModel {

    playerId: number;
    playerNickName: string;
    token: string;
    askAboutChangePassword: boolean;
    isBanned: boolean;

}