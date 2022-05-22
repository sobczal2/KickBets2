import { BaseDto } from "./base";

export interface TeamDto extends BaseDto {
    name: string;
    logo: string | null;
}