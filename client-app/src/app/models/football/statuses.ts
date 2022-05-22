import { BaseDto } from "./base";

export interface StatusDto extends BaseDto {
    long: string;
    short: string;
    elapsed: number | null;
}