export type Transaction = {
  id: number;
  productId: number;
  subtotal: number;
  tax: number;
  total: number;
  type: number;
  quantity: number;
  transactionLabel: string;
  createdDate: Date;
};