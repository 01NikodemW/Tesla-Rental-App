import axios, { AxiosRequestConfig } from "axios";
import router from "next/router";

export function getJWTHeader(): Record<string, string> {
  const token = localStorage.getItem("accessToken");

  if (!token) {
    router.push("/authentication/login");
  }
  return { Authorization: `Bearer ${token}` };
}

const config: AxiosRequestConfig = {
  baseURL: process.env.NEXT_PUBLIC_API_ENTRYPOINT,
};
export const axiosInstance = axios.create(config);
