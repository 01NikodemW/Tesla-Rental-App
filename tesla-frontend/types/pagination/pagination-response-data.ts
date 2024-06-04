export type PaginationResponseData<T> = {
  items: T[];
  totalPages: number;
  totalItemsCount: number;
  itemsFrom: number;
  itemsTo: number;
};
