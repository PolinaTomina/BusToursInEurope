export interface CreateReviewDto {
  tourId: number;
  rating: number;
  comment: string | null;
}

export interface ReviewDto {
  id: number;
  login: string | null;
  comment: string | null;
  rating: number;
  reviewDate: string;
  userId: number;
  tourId: number;
}