import { QueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { t } from "i18next";

export function queryErrorHandler(error: any): void {
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
