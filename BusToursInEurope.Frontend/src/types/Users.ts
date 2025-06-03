import { ReservationDto } from './Reservations';
import { ReviewDto } from './Reviews';

export interface UserDto {
  id: number;
  email: string | null;
  login: string | null;
  password: string | null;
  role: string | null;
  reservationsDto: ReservationDto[];
  reviewsDto: ReviewDto[];
}

export interface ShortUserDto {
  Id: number;
  Email: string;
}