import { useQuery } from "@tanstack/react-query";
import { queryKeys } from "@/api/query-keys";
import { axiosInstance } from "@/api/axios-instance";
import { AvailableCarsRequest } from "@/types/api/cars/available-cars-request";
import { Car } from "@/types/car";

export async function getAvailableCars(
  data: AvailableCarsRequest
): Promise<Car[]> {
  let url = `/api/${queryKeys.cars}/${queryKeys.available}`;
  if (data.rentalDate && data.returnDate) {
    url += `?rentalDate=${data.rentalDate}&returnDate=${data.returnDate}`;
  }

  const response = await axiosInstance.get(url, {
    headers: {
      "Content-Type": "application/json",
    },
  });

  console.log("response", response.data);

  return response.data;
}

export function useAvailableCars(date: AvailableCarsRequest) {
  const { data: availableCars = [], isFetching: isAvailableCarsFetching } =
    useQuery<Car[]>({
      queryKey: [
        queryKeys.cars,
        queryKeys.available,
        date.rentalDate,
        date.returnDate,
      ],
      queryFn: () => getAvailableCars(date),
    });

  return {
    availableCars,
    isAvailableCarsFetching,
  };
}
