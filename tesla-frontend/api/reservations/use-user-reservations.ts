import { useQuery } from "@tanstack/react-query";
import { queryKeys } from "@/api/query-keys";
import { axiosInstance, getJWTHeader } from "@/api/axios-instance";
import { Reservation } from "@/types/reservation";

export async function getUserReservations(): Promise<Reservation[]> {
  console.log("jere");
  const response = await axiosInstance.get(
    `/api/${queryKeys.users}/${queryKeys.reservations}`,
    {
      headers: {
        "Content-Type": "application/json",
        ...getJWTHeader(),
      },
    }
  );

  console.log("response", response);

  return response.data;
}

export function useUserReservations() {
  const {
    data: userReservations = [],
    isFetching: isUserReservationsFetching,
  } = useQuery<Reservation[]>({
    queryKey: [queryKeys.users, queryKeys.reservations],
    queryFn: () => getUserReservations(),
  });

  return {
    userReservations,
    isUserReservationsFetching,
  };
}
