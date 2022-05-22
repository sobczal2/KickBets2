import { BaseDto } from "./base";

export interface ScoreDto extends BaseDto {
    homeCurrentScore: number | null;
    awayCurrentScore: number | null;
    homeHalfTime: number | null;
    awayHalfTime: number | null;
    homeFullTime: number | null;
    awayFullTime: number | null;
    homeExtraTime: number | null;
    awayExtraTime: number | null;
    homePenalty: number | null;
    awayPenalty: number | null;
}