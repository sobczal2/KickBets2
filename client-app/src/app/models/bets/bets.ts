import { BaseDto } from "../common/base"
import {WdlhtBetsDataDto} from "./wdlhtBets";
import {WdlftBetsDataDto} from "./wdlftBets";

export interface BetsDataDto extends BaseDto {
    wdlhtBetsData: WdlhtBetsDataDto;
    wdlftBetsData: WdlftBetsDataDto;
}

export interface BaseBetDto extends BaseDto {
    fixtureId: number;
    value: number;
    timeStamp: string;
    status: string;
    type: string;
    betsDataId: number;
    homeTeamName: string;
    awayTeamName: string;
    description: string;
}