/*
 * Ems.WebApp.Front
 *
 * (c) 2022 Ejsak Gorup
 */
import { QueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { useTranslation } from "react-i18next";
import { t } from "i18next";

export function queryErrorHandler(error: any): void {
  console.log(error);

  if ("errors" in error) {
    const errors = error.errors;

    for (const key in errors) {
      if (errors.hasOwnProperty(key)) {
        errors[key].forEach((message: string) => {
          toast.error(t(message));
        });
      }
    }
  } else if ("message" in error) {
    toast.error(t(error.message));
    return;
  }
}

export const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      staleTime: 3600000,
      refetchOnWindowFocus: false,
    },
  },
});
