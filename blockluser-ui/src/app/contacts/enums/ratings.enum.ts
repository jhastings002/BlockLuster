export enum Ratings {
  G = 0,
  PG = 1,
  PG13 = 2,
  R = 3,
  NC17 = 4,
}

export const RatingsMapper = {
  [Ratings.G]: 'G',
  [Ratings.PG]: 'PG',
  [Ratings.PG13]: 'PG13',
  [Ratings.R]: 'R',
  [Ratings.NC17]: 'NC17',
};
