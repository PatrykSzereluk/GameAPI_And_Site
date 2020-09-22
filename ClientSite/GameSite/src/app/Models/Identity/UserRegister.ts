export interface UserRegisterResponseModel {
    isSuccess: boolean;
    message: string;
    statusCode: number;
    playerId: number;
    nickName: string;
}
export class UserRegisterRequestModel {
    Login: string;
    NickName: string;
    Password: string;
    Email: string;
}
