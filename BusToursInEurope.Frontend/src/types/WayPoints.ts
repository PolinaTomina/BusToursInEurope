export interface CreateWPDto {
  description: string;
  routeBusDtoId: number;
}

export interface CreateWayPointDto {
  description: string | null;
}

export interface WayPointDto {
  id: number;
  description: string;
  routeBusDtoId: number;
}