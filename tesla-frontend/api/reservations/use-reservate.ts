import { useMutation } from "@tanstack/react-query";
import { axiosInstance, getJWTHeader } from "@/api/axios-instance";
import { queryClient, queryErrorHandler } from "../query-client";
import { queryKeys } from "../query-keys";
import { ReservateCarRequest } from "@/types/api/reservations/reservate-car-request";

export async function reservateCar(data: ReservateCarRequest) {
  const response = await axiosInstance.post(
    `/api/${queryKeys.reservations}`,
    data,
    {
      headers: {
        "Content-Type": "application/json",
        ...getJWTHeader(),
      },
    }
  );

  return response.data;
}

export function useReservateCar(onReservateSuccess: () => void) {
  const { mutate } = useMutation({
    mutationFn: (data: ReservateCarRequest) => reservateCar(data),
    onSuccess: () => {
      onReservateSuccess();
      queryClient.invalidateQueries({ queryKey: [queryKeys.cars] });
      queryClient.invalidateQueries({ queryKey: [queryKeys.users] });
    },
    onError: (error: any) => {
      queryErrorHandler(error.response.data);
    },
  });

  return mutate;
}
