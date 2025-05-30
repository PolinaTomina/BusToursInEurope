export interface RegistrationDto {
  login: string | null;
  password: string | null;
  email: string | null;
  numPhone: string | null;
}

export interface AuthorizationDto {
  password: string | null;
  email: string | null;
}