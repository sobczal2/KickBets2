export interface LoginDto {
    email: string;
    password: string;
}

export interface RegisterDto {
    userName: string;
    email: string;
    password: string;
}

export interface UserDto {
    userName: string;
    email: string;
    balance: string;
    balanceAddAvailableAt: Date;
    token: string | null;
}