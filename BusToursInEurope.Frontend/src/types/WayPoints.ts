import { CityDto } from './Cities';
import { HotelDto } from './Hotels';
import { RouteBusDto } from './Routes';

export interface CreateWPDto {
  namePlace: string | null;
  cityDtoId: number;
  routeBusDtoId: number;
  hotelDtoId: number;
}

export interface CreateWayPointDto {
  namePlace: string | null;
  cityId: number;
  hotelId: number;
}

export interface WayPointDto {
  id: number;
  namePlace: string | null;
  cityDtoId: number;
  cityDto: CityDto;
  routeBusDtoId: number;
  routeBusDto: RouteBusDto;
  hotelDtoId: number;
  hotelDto: HotelDto;
}