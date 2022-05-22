import {BaseDto} from "./base";

export interface LineupDto extends BaseDto {
    teamId: number;
    formation: string;
    coachName: string;
    coachPhoto: string;
}