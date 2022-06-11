import {BaseDto} from "../common/base"

export interface WdlhtBetsDataDto extends BaseDto {
    homeBetsMultiplier: number;
    drawBetsMultiplier: number;
    awayBetsMultiplier: number;
}