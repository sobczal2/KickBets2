import {BaseDto} from "./base";

export interface LeagueDto extends BaseDto {
    name: string;
    country: string;
    logo: string | null;
    flag: string | null;
}