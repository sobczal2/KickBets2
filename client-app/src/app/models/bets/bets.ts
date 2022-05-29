import { BaseDto } from "../common/base"
import {WdlhtBetsDataDto} from "./wdlhtBets";
import {WdlftBetsDataDto} from "./wdlftBets";

export interface BetsDataDto extends BaseDto {
    wdlhtBetsData: WdlhtBetsDataDto;
    wdlftBetsData: WdlftBetsDataDto;
}