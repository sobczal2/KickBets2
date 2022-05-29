import { BaseDto } from "../common/base";

export interface TeamDto extends BaseDto {
    name: string;
    logo: string | null;
}