export interface CreateReservationDto {
  numOfSeats: number;
  tourId: number;
}

export interface ReservationDto {
  id: number;
  date: string;
  paymentDate?: string;
  paymentDeadline: string;
  numOfSeats: number;
  userId: number;
}

export interface UpdatePaymentStatusDto {
  id: number;
  isPaid: boolean;
}