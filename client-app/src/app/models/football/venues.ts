import {BaseDto} from "../common/base";

export interface VenueDto extends BaseDto {
    name: string;
    address: string;
    city: string;
    country: string;
    capacity: number;
    surface: string;
    image: string | null;
}