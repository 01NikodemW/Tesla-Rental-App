import { useQuery } from "@tanstack/react-query";
import { queryKeys } from "@/api/query-keys";
import { axiosInstance, getJWTHeader } from "@/api/axios-instance";
import { Country } from "@/types/country";

export async function getCountries(): Promise<Country[]> {
  const response = await axiosInstance.get(`/api/${queryKeys.countries}`, {
    headers: {
      "Content-Type": "application/json",
    },
  });

  return response.data;
}

export function useCountries() {
  const { data: countries = [], isFetching: isCountriesFetching } = useQuery<
    Country[]
  >({
    queryKey: [queryKeys.countries],
    queryFn: () => getCountries(),
  });

  return {
    countries,
    isCountriesFetching,
  };
}
