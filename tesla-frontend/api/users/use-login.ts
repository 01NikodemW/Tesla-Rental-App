import { useMutation } from "@tanstack/react-query";
import { axiosInstance } from "@/api/axios-instance";
import { queryErrorHandler } from "../query-client";
import { queryKeys } from "../query-keys";
import toast from "react-hot-toast";
import { useTranslation } from "react-i18next";
import { LoginRequest } from "@/types/api/users/login-request";

export async function login(data: LoginRequest) {
  console.log("here");
  const response = await axiosInstance.post(
    `/api/${queryKeys.users}/login`,
    data,
    {
      headers: {
        "Content-Type": "application/json",
      },
    }
  );

  return response.data;
}

export function useLogin(onLoginSuccess: () => void) {
  const { t } = useTranslation();
  const { mutate } = useMutation({
    mutationFn: (data: LoginRequest) => login(data),
    onSuccess: (data, variables) => {
      localStorage.setItem("accessToken", data.token);
      toast.success(t("Welcome back!"));
      onLoginSuccess();
    },
    onError: (error: any) => {
      queryErrorHandler(error.response.data);
    },
  });

  return mutate;
}
