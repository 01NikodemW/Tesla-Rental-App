import { useQuery } from "@tanstack/react-query";
import { queryKeys } from "@/api/query-keys";
import { axiosInstance } from "@/api/axios-instance";
import { Location } from "@/types/location";

export async function getLocations(): Promise<Location[]> {
  const response = await axiosInstance.get(`/api/${queryKeys.locations}`, {
    headers: {
      "Content-Type": "application/json",
    },
  });

  return response.data;
}

export function useLocations() {
  const { data: locations = [], isFetching: isLocationsFetching } = useQuery<
    Location[]
  >({
    queryKey: [queryKeys.locations],
    queryFn: () => getLocations(),
  });

  return {
    locations,
    isLocationsFetching,
  };
}
