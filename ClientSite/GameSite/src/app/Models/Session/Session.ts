export class Session {
    user: User;
    token: string;
    gameToken: string;
}

export class User {
    playerId: number;
    nickName: string;
}
