import { UserDto } from "./Users";

export interface ProfileDto {
  id: number;
  name: string | null;
  middleName: string | null;
  surName: string | null;
  numPhone: string | null;
  passportNumber: string | null;
  user: UserDto;
}

export interface UpdateProfileDto {
    name?: string;
    surName?: string;
    middleName?: string;
    numPhone?: string;
    passportNumber?: string;
}