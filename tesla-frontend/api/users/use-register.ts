import { useMutation } from "@tanstack/react-query";
import { axiosInstance } from "@/api/axios-instance";
import { queryErrorHandler } from "../query-client";
import { queryKeys } from "../query-keys";
import { RegisterRequest } from "@/types/api/users/register-request";

export async function register(data: RegisterRequest) {
  const response = await axiosInstance.post(
    `/api/${queryKeys.users}/register`,
    data,
    {
      headers: {
        "Content-Type": "application/json",
      },
    }
  );

  return response.data;
}

export function useRegister(onRegisterSuccess: () => void) {
  const { mutate } = useMutation({
    mutationFn: (data: RegisterRequest) => register(data),
    onSuccess: () => {
      onRegisterSuccess();
    },
    onError: (error: any) => {
      queryErrorHandler(error.response.data);
    },
  });

  return mutate;
}
