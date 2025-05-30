import { BusDto } from './Buses';
import { ReservationDto } from './Reservations';
import { ReviewDto } from './Reviews';
import { RouteBusDto } from './Routes';

export interface CreateTourDto {
  id: number;
  name: string | null;
  price: number;
  startDate: string;
  endDate: string;
  numOfSeats: number;
  description: string | null;
  images: File[];
  busId: number;
  routeBusId: number;
}

export interface UpdateTourDto {
  name: string | null;
  price: number | null; // CHANGED: сделал nullable как в бэкенде
  startDate: string | null; // CHANGED: сделал nullable
  endDate: string | null; // CHANGED: сделал nullable
  route: string | null;
  numOfSeats: number | null; // CHANGED: сделал nullable
  description: string | null;
  images?: File[]; // NEW: добавил поддержку изображений
  busId: number;
  routeBusId: number
}

export interface ShortTourDto {
  id: number;
  name: string | null;
  price: number;
  startDate: string;
  endDate: string;
  firstImageLink: string;
}

export interface FullTourDto {
  id: number;
  name: string | null;
  price: number;
  startDate: string;
  endDate: string;
  numOfSeats: number;
  description: string | null;
  busDto: BusDto;
  routeBusDto: RouteBusDto;
  reservationsDto: ReservationDto[] | null;
  reviewsDto: ReviewDto[] | null;
  fullImageLink: string[]
}