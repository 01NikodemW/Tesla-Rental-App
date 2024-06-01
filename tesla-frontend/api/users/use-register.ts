import { Register, useMutation } from "@tanstack/react-query";
import { axiosInstance, getJWTHeader } from "@/api/axios-instance";
import { queryClient, queryErrorHandler } from "../query-client";
import { queryKeys } from "../query-keys";
import toast from "react-hot-toast";
import { useTranslation } from "react-i18next";
import { ErrorType } from "../api-error";
import { LoginRequest } from "@/types/api/users/login-request";
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
  const { t } = useTranslation();
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
