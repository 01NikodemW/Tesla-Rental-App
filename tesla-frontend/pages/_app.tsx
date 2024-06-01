import { queryClient } from "@/api/query-client";
import "@/styles/globals.css";
import { theme } from "@/theme/theme-options";
import ThemeProvider from "@mui/system/ThemeProvider";
import { QueryClientProvider } from "@tanstack/react-query";
import type { AppProps } from "next/app";
import { Toaster } from "react-hot-toast";
import "../i18n";

export default function App({ Component, pageProps }: AppProps) {
  return (
    <QueryClientProvider client={queryClient}>
      <ThemeProvider theme={theme}>
        <Toaster position="top-center" />
        <Component {...pageProps} />
      </ThemeProvider>
    </QueryClientProvider>
  );
}
