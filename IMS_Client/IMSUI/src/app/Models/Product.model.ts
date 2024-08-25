export interface Product {
  productId: string; 
  name: string;
  price: number;
  availableQuantity: number;
  description: string;
  categoryName: string;
  imageUrl?: string | null; 
  imageLocalPath?: string | null;
  image?: File | null; 
}

