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

export default function Home() {
  const [rentalDate, setRentalDate] = useState<Date | null>(null);
  const [returnDate, setReturnDate] = useState<Date | null>(null);
  const [isSnackbarOpen, setIsSnackbarOpen] = useState<boolean>(false);
  const [snackbarMessage, setSnackbarMessage] = useState<string>("");
  const [isLoggedIn, setIsLoggedIn] = useState<boolean>(false);
  const [currentImageIndex, setCurrentImageIndex] = useState(0);
  const router = useRouter();
  const { t } = useTranslation();

  const [backgroundImages, setBackgroundImages] = useState([
    "https://cdn.motor1.com/images/mgl/JlnNn/s1/tesla-model-s-plaid.jpg",
    "https://electricmobility.store/wp-content/uploads/2020/06/gf-26jY-UXvx-TSb4_tesla-model-s-p100d-1920x1080-nocrop.jpg",
    "https://wallpaperaccess.com/full/486595.jpg",
    "https://images.hdqwalls.com/download/tesla-model-x-front-4k-5x-1920x1080.jpg",
  ]);

  const [loginOrRegister, setLoginOrRegister] = useState<
    "login" | "register" | "None"
  >("register");
  useEffect(() => {
    const intervalId = setInterval(() => {
      setCurrentImageIndex((currentIndex) =>
        currentIndex === backgroundImages.length - 1 ? 0 : currentIndex + 1
      );
    }, 5000);

    return () => clearInterval(intervalId);
  }, [backgroundImages.length]);

  const setLogin = () => {
    setLoginOrRegister("login");
  };

  const setRegister = () => {
    setLoginOrRegister("register");
  };

  const setNone = () => {
    setLoginOrRegister("None");
  };

  useEffect(() => {
    if (localStorage.getItem("token")) {
      setIsLoggedIn(true);
    } else {
      setIsLoggedIn(false);
    }
  }, [loginOrRegister]);

  const handleSnackbarClose = () => {
    setIsSnackbarOpen(false);
    setSnackbarMessage("");
  };

  const disableSearch =
    !rentalDate ||
    !returnDate ||
    rentalDate > returnDate ||
    returnDate < new Date() ||
    rentalDate < new Date();

  return (
    <>
      <Box
        sx={{
          backgroundImage: `url(${backgroundImages[currentImageIndex]})`,
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
                  localStorage.setItem("token", "");
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
                  label="Rental date"
                  value={rentalDate}
                  onChange={(newValue) => setRentalDate(newValue)}
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
                  label="Return date"
                  value={returnDate}
                  onChange={(newValue) => setReturnDate(newValue)}
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
                disabled={disableSearch}
                onClick={() => {
                  if (rentalDate && returnDate) {
                    if (!isLoggedIn) {
                      setLogin();
                      return;
                    }
                    localStorage.setItem("rentalDate", rentalDate.toString());
                    localStorage.setItem("returnDate", returnDate.toString());
                    if (rentalDate > returnDate) {
                      setIsSnackbarOpen(true);
                      setSnackbarMessage(
                        "Return date must be after rental date"
                      );
                      return;
                    }
                    router.push({
                      pathname: "/available-vehicles",
                    });
                  }
                }}
              >
                Search
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
          <Snackbar
            open={isSnackbarOpen}
            autoHideDuration={6000}
            onClose={handleSnackbarClose}
            message={snackbarMessage}
          />
        </Box>
      </Box>
    </>
  );
}
