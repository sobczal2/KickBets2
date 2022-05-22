import {BaseDto} from "./base";

export interface PlayerDto extends BaseDto {
    name: string;
    number: number;
    pos: string;
    gridX: number | null;
    gridY: number | null;
    starting11: boolean;
}