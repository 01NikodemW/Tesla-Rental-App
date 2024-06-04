import { useQuery } from "@tanstack/react-query";
import { queryKeys } from "@/api/query-keys";
import { axiosInstance, getJWTHeader } from "@/api/axios-instance";
import { Reservation } from "@/types/reservation";
import { PaginationRequestData } from "@/types/pagination/pagination-request-data";
import { PaginationResponseData } from "@/types/pagination/pagination-response-data";

export async function getUserReservations(
  data: PaginationRequestData
): Promise<PaginationResponseData<Reservation>> {
  const url = `/api/${queryKeys.users}/${queryKeys.reservations}?pageNumber=${data.pageNumber}&pageSize=${data.pageSize}&sortBy=${data.sortBy}&sortDirection=${data.sortDirection}`;
  const response = await axiosInstance.get(url, {
    headers: {
      "Content-Type": "application/json",
      ...getJWTHeader(),
    },
  });

  console.log("response", response);

  return response.data;
}

export function useUserReservations(data: PaginationRequestData) {
  const {
    data: userReservations = {
      items: [],
      totalPages: 0,
      totalItemsCount: 0,
      itemsFrom: 0,
      itemsTo: 0,
    },
    isFetching: isUserReservationsFetching,
  } = useQuery<PaginationResponseData<Reservation>>({
    queryKey: [queryKeys.users, queryKeys.reservations, data],
    queryFn: () => getUserReservations(data),
  });

  return {
    userReservations,
    isUserReservationsFetching,
  };
}
