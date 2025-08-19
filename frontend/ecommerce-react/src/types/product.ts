
export type ProductImage = {
  id: number;
  url: string;
};

export type Product = {
  id: number;
  name: string;
  price: number;
  description: string;
  stock: number;
  statusLabel?: string;
  categoryId?: string;
  images: ProductImage[];
};

export type ProductStatus = "active" | "inactive";

export const ProductStatusValues: Record<string, ProductStatus> = {
  Active: "active",
  Inactive: "inactive",
};


export interface ProductFilters {
  CategoryId?: number;
  PriceMin?: number;
  PriceMax?: number;
  Status?: string; 
}

export interface SearchParams {
  search: string;
  priceMin?: number;
  priceMax?: number;
}