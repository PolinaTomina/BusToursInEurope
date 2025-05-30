export interface CityDto {
  id: number;
  name: string | null;
  country: string | null;
  visa: boolean;
  hotelIds: number[] | null;
  wayPointIds: number[] | null;
}