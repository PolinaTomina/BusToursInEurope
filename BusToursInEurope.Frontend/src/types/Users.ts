import { ReservationDto } from './Reservations';
import { ReviewDto } from './Reviews';

export interface UserDto {
  id: number;
  email: string | null;
  fullName: string | null;
  userName: string | null;
  login: string | null;
  password: string | null;
  numPhone: string | null;
  isAdmin: boolean;
  isUser: boolean;
  reservationsDto: ReservationDto[] | null;
  reviewsDto: ReviewDto[] | null;
}