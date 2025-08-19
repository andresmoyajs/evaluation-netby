import type { Product } from "./product";

export type ApiResponse = {
  count: number;
  pageIndex: number;
  pageSize: number;
  data: Product[];
  pageCount: number;
};