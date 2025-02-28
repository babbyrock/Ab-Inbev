export interface ProductQueryParams {
  pageNumber: number;
  pageSize: number;
  title?: string;
  minPrice?: number;
  maxPrice?: number;
  category?: string;
  description?: string;
  image?: string;
  count?: number;
  rate?: number;
}
