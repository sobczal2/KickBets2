import {BaseDto} from "../common/base"

export interface WdlftBetsDataDto extends BaseDto {
    homeBetsMultiplier: number;
    drawBetsMultiplier: number;
    awayBetsMultiplier: number;
}