import {BaseDto} from "../common/base";

export interface StatisticDto extends BaseDto {
    shotsOnGoal: number | null;
    shotsOffGoal: number | null;
    shotsInsideBox: number | null;
    shotsOutsideBox: number | null;
    totalShots: number | null;
    blockedShots: number | null;
    fouls: number | null;
    cornerKicks: number | null;
    offsides: number | null;
    possession: number | null;
    yellowCards: number | null;
    redCards: number | null;
    goalkeeperSaves: number | null;
    totalPasses: number | null;
    accuratePasses: number | null;
    passes: number | null;
}