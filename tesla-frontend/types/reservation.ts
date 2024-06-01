export type Reservation = {
  id: string;
  userId: string;
  car: {
    id: string;
    model: string;
    mileage: number;
    imageUrl: string;
    rentalPricePerDay: number;
  };
  rentalLocation: {
    id: string;
    name: string;
  };
  returnLocation: {
    id: string;
    name: string;
  };
  rentalDate: string;
  returnDate: string;
  totalPrice: number;
};
