import { CreateWayPointDto, WayPointDto } from './WayPoints';

export interface CreateRouteBusDto {
  distance: number;
  wayPoints: CreateWayPointDto[] | null;
}

export interface RouteBusDto {
  id: number;
  distance: number;
  wayPointsDto: WayPointDto[] | null;
}

export interface UpdateRouteBusDto {
  distance: number;
  wayPoints: CreateWayPointDto[] | null;
  id: number;
}