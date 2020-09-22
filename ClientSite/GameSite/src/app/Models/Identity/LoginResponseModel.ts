export interface LoginResponseModel {

    playerId: number;
    playerNickName: string;
    token: string;
    gameToken: string;
    askAboutChangePassword: boolean;
    isBanned: boolean;

}