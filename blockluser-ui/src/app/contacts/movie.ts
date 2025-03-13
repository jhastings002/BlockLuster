import { Ratings } from './enums/ratings.enum';

export interface Movie {
  Id?: string;
  Title?: string;
  Description?: string;
  Rating: Ratings;
  DailyRate?: number;
  PictureLocation?: string;
  IsAvailable?: boolean;
}
