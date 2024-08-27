export interface AddNewProduct{
     name: string,
    price: number,
    description: string,
    availableQuantity:number,
    categoryID: string,
    imageUrl: string,
    imageLocalPath?:string,
}