export type Reservation = {
  id: string;
  userId: string;
  vehicle: {
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
  rentalDate: Date;
  returnDate: Date;
  totalPrice: number;
};
