import {BaseDto} from "../common/base";
import {Dayjs} from "dayjs";

export interface FixtureDto extends BaseDto {
    referee: string | null;
    date: Dayjs;
    venueId: number | null;
    statusId: number;
    leagueId: number;
    homeTeamId: number | null;
    awayTeamId: number | null;
    homeLineupId: number | null;
    awayLineupId: number | null;
    scoreId: number;
    homeStatisticsId: number | null;
    awayStatisticsId: number | null;
    betsDataId: number;
}

export interface FixtureListParams {
    type: "all" | "upcoming" | "ended" | "cancelled";
    leagueId: number | null;
}