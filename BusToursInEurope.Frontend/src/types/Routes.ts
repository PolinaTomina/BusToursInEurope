import { CreateWayPointDto, WayPointDto } from './WayPoints';

export interface CreateRouteBusDto {
  name: string
  distance: number;
  wayPoints: CreateWayPointDto[] | null;
}

export interface RouteBusDto {
  id: number;
  name: string;
  distance: number;
  wayPointsDto: WayPointDto[] | null;
}

export interface UpdateRouteBusDto {
  name: string;
  distance: number;
  wayPoints: CreateWayPointDto[] | null;
  id: number;
}