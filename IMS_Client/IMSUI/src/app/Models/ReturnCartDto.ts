import { ReturnProductFromCartDto } from "./ReturnProductFromCart.model";
export interface ReturnCartDto {
  id: string; // Assuming backend GUID maps to string in TypeScript
  totalProductQty: number;
  totalValue: number;
  products: ReturnProductFromCartDto[];
}