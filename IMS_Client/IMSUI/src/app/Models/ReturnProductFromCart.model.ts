export interface ReturnProductFromCartDto {
    productId: string; // Assuming backend GUID maps to string in TypeScript
    name: string;
    price: number;
    categoryName: string;
    productCount: number;
  }