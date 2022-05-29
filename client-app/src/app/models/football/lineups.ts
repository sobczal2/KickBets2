import {BaseDto} from "../common/base";

export interface LineupDto extends BaseDto {
    teamId: number;
    formation: string;
    coachName: string;
    coachPhoto: string;
}