import { categories } from "@/types/category";

export const getCategoryName = (id: number) => {
  const category = categories.find((c) => c.id === id);
  return category ? category.name : "Sin categoría";
};