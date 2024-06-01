export type ReservateCarRequest = {
  carId: string;
  rentalLocationId?: string;
  returnLocationId?: string;
  rentalDate: string;
  returnDate: string;
};
