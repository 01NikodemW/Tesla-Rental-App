import Head from "next/head";
import Image from "next/image";
import { Inter } from "next/font/google";
import styles from "@/styles/Home.module.css";
import { Box, Button, Snackbar, TextField, Typography } from "@mui/material";
import LocationOnIcon from "@mui/icons-material/LocationOn";
import LocalPhoneIcon from "@mui/icons-material/LocalPhone";
import LoginIcon from "@mui/icons-material/Login";
import LogoutIcon from "@mui/icons-material/Logout";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider";
import { DatePicker } from "@mui/x-date-pickers/DatePicker";
import { useEffect, useState } from "react";
import { useRouter } from "next/router";
import { DateField } from "@mui/x-date-pickers/DateField";
import { Login } from "@/components/home/login";
import { Register } from "@/components/home/register";
import { useTranslation } from "react-i18next";
import dayjs, { Dayjs } from "dayjs";
import toast from "react-hot-toast";

export default function Home() {
  const [rentalDate, setRentalDate] = useState<Date | null>(null);
  const [returnDate, setReturnDate] = useState<Date | null>(null);
  const [isLoggedIn, setIsLoggedIn] = useState<boolean>(false);
  const router = useRouter();
  const { t } = useTranslation();

  const [loginOrRegister, setLoginOrRegister] = useState<
    "login" | "register" | "none"
  >("none");

  const setLogin = () => {
    setLoginOrRegister("login");
  };

  const setRegister = () => {
    setLoginOrRegister("register");
  };

  const setNone = () => {
    setLoginOrRegister("none");
  };

  useEffect(() => {
    if (localStorage.getItem("accessToken")) {
      setIsLoggedIn(true);
    } else {
      setIsLoggedIn(false);
    }
  }, [loginOrRegister]);

  return (
    <>
      <Box
        sx={{
          backgroundImage: `url(https://images.hdqwalls.com/download/tesla-model-x-front-4k-5x-1920x1080.jpg)`,
          backgroundSize: "cover",
          backgroundPosition: "center",
          height: "100vh",
          transition: "background-image 3s ease-in-out",
        }}
      >
        <Box
          sx={{
            height: "100px",
            borderBottom: "1px solid white",
            display: "flex",
            justifyContent: "space-between",
            alignItems: "center",
            px: 6,
          }}
        >
          <Typography
            sx={{
              fontSize: "2rem",
              fontWeight: "300",
              cursor: "pointer",
            }}
            onClick={() => {
              router.push("/");
            }}
          >
            Mallorca Tesla Rental
          </Typography>
          {isLoggedIn && (
            <Button
              onClick={() => {
                router.push("/my-reservations");
              }}
            >
              {t("Show my rentals")}
            </Button>
          )}
          <Box
            sx={{
              display: "flex",
            }}
          >
            <Box sx={{ display: "flex", justifyContent: "space-between" }}>
              <Box sx={{ display: "flex" }}>
                <LocalPhoneIcon style={{ fontSize: 20, marginTop: 2 }} />
                <Box sx={{ ml: 1 }}>
                  <Typography>+34 123 456 789</Typography>
                  <Typography>+34 123 456 789</Typography>
                </Box>
              </Box>

              <Box
                sx={{
                  width: "1px",
                  height: "100%",
                  bgcolor: "white",
                  mx: 4,
                }}
              />

              <Box sx={{ display: "flex" }}>
                <LocationOnIcon style={{ fontSize: 20, marginTop: 2 }} />
                <Box sx={{ ml: 1 }}>
                  <Typography>57 Rue Alcudia</Typography>
                  <Typography>32 Mau Manacor</Typography>
                </Box>
              </Box>
            </Box>

            {!isLoggedIn && (
              <Button
                endIcon={<LoginIcon />}
                sx={{ color: "white", ml: 4, border: "1px solid white", px: 2 }}
                onClick={setLogin}
              >
                {t("Login")}
              </Button>
            )}
            {isLoggedIn && (
              <Button
                endIcon={<LogoutIcon />}
                sx={{ color: "white", ml: 4, border: "1px solid white", px: 2 }}
                onClick={() => {
                  localStorage.setItem("accessToken", "");
                  router.push("/");
                  setIsLoggedIn(false);
                }}
              >
                {t("Logout")}
              </Button>
            )}
          </Box>
        </Box>
        <Box
          sx={{
            px: 6,
            display: "flex",
          }}
        >
          <Box
            sx={{
              width: "50%",
            }}
          >
            <Typography
              variant="h3"
              sx={{
                pt: 8,
                mb: 2,
              }}
            >
              {t("Check available Tesla cars")}
            </Typography>
            <Box
              sx={{
                display: "flex",
                alignItems: "center",
              }}
            >
              <LocalizationProvider dateAdapter={AdapterDayjs}>
                <DateField
                  label={t("Rental date")}
                  value={rentalDate ? dayjs(rentalDate) : null}
                  onChange={(newValue: Dayjs | null) => {
                    if (newValue) {
                      setRentalDate(newValue.toDate());
                    }
                  }}
                  format="DD-MM-YYYY"
                  sx={{
                    mr: 2,
                    "& .MuiOutlinedInput-root": {
                      "& fieldset": {
                        borderColor: "#ffffff",
                      },
                      "&:hover fieldset": {
                        borderColor: "#ffffff",
                      },
                      "&.Mui-focused fieldset": {
                        borderColor: "#ffffff",
                      },
                    },
                    "& .MuiOutlinedInput-input": {
                      color: "#ffffff",
                    },
                    "& .MuiInputLabel-root": {
                      color: "#fff",
                    },
                  }}
                />
              </LocalizationProvider>

              <LocalizationProvider dateAdapter={AdapterDayjs}>
                <DateField
                  label={t("Return date")}
                  value={returnDate ? dayjs(returnDate) : null}
                  onChange={(newValue: Dayjs | null) => {
                    if (newValue) {
                      setReturnDate(newValue.toDate());
                    }
                  }}
                  format="DD-MM-YYYY"
                  sx={{
                    mr: 2,
                    "& .MuiOutlinedInput-root": {
                      "& fieldset": {
                        borderColor: "#ffffff",
                      },
                      "&:hover fieldset": {
                        borderColor: "#ffffff",
                      },
                      "&.Mui-focused fieldset": {
                        borderColor: "#ffffff",
                      },
                    },
                    "& .MuiOutlinedInput-input": {
                      color: "#ffffff",
                    },
                    "& .MuiInputLabel-root": {
                      color: "#fff",
                    },
                  }}
                />
              </LocalizationProvider>

              <Button
                sx={{
                  border: "1px solid white",
                  color: "white",
                  height: "56px",
                  px: 2,
                  "&.Mui-disabled": {
                    border: "1px solid white",
                    color: "white",
                  },
                }}
                onClick={() => {
                  if (rentalDate && returnDate) {
                    if (!isLoggedIn) {
                      setLogin();
                      return;
                    }

                    if (
                      dayjs(rentalDate).isBefore(dayjs(), "day") ||
                      dayjs(rentalDate).isSame(dayjs(), "day")
                    ) {
                      toast.error(
                        t(
                          "Rental date and return date must be greater than today"
                        )
                      );
                      return;
                    }

                    if (dayjs(rentalDate).isAfter(dayjs(returnDate), "day")) {
                      toast.error(
                        t("Rental date must be less than return date")
                      );
                      return;
                    }

                    const rentalDatePlusOne = new Date(rentalDate);
                    rentalDatePlusOne.setDate(rentalDatePlusOne.getDate() + 1);

                    const returnDatePlusOne = new Date(returnDate);
                    returnDatePlusOne.setDate(returnDatePlusOne.getDate() + 1);

                    const rentalDateFormatted = rentalDatePlusOne
                      .toISOString()
                      .split("T")[0];

                    const returnDateFormatted = returnDatePlusOne
                      .toISOString()
                      .split("T")[0];

                    router.push(
                      `/available-cars?rentalDate=${rentalDateFormatted}&returnDate=${returnDateFormatted}`
                    );
                  }
                }}
              >
                {t("Search")}
              </Button>
            </Box>
          </Box>
          <Box
            sx={{
              display: "flex",
              justifyContent: "end",
              mr: 4,
              width: "50%",
              pt: 8,
            }}
          >
            {loginOrRegister === "login" && (
              <Login setNone={setNone} setRegister={setRegister} />
            )}
            {loginOrRegister === "register" && <Register setLogin={setLogin} />}
          </Box>
        </Box>
      </Box>
    </>
  );
}
