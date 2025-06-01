import { UserDto } from './Users';

export interface CreateReservationDto {
  numOfSeats: number;
  tourId: number;
}

export interface ReservationDto {
  id: number;
  date: string;
  paymentDate: string;
  paymentDeadline: string;
  numOfSeats: number;
  userDtoId: number;
  userDto: UserDto;
}