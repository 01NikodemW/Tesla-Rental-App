import { Box, Button, Grid, MenuItem, Select, Typography } from "@mui/material";
import LocationOnIcon from "@mui/icons-material/LocationOn";
import LocalPhoneIcon from "@mui/icons-material/LocalPhone";
import { useEffect, useState } from "react";
import { useRouter } from "next/router";
import LogoutIcon from "@mui/icons-material/Logout";
import { useUserReservations } from "@/api/reservations/use-user-reservations";
import { ReservedCar } from "@/components/my-reservations/reserved-car";
import { useTranslation } from "react-i18next";
import { PaginationRequestData } from "@/types/pagination/pagination-request-data";
import { queryClient } from "@/api/query-client";
import { queryKeys } from "@/api/query-keys";

export default function MyReservations() {
  const router = useRouter();
  const [isAuthenticated, setIsAuthenticated] = useState<boolean>(false);
  const { t } = useTranslation();

  const [reservationsSearchParams, setReservationsSearchParams] =
    useState<PaginationRequestData>({
      pageNumber: 1,
      pageSize: 20,
      sortBy: "RentalDate",
      sortDirection: "Ascending",
    });

  const sortByOptions = ["Car", "RentalDate", "ReturnDate", "TotalPrice"];

  const generateSortByOptionsLabel = (name: string) => {
    switch (name) {
      case "Car":
        return t("Tesla model");
      case "RentalDate":
        return t("Rental date");
      case "ReturnDate":
        return t("Return date");
      case "TotalPrice":
        return t("Total price");
      default:
        return name;
    }
  };

  const { userReservations, isUserReservationsFetching } = useUserReservations(
    reservationsSearchParams
  );

  useEffect(() => {
    if (localStorage.getItem("accessToken")) {
      setIsAuthenticated(true);
    } else {
      setIsAuthenticated(false);
    }
  }, [router]);

  return (
    <>
      {isAuthenticated && (
        <Box
          sx={{
            backgroundImage: `url(https://images.hdqwalls.com/download/tesla-model-x-front-4k-5x-1920x1080.jpg)`,
            backgroundSize: "cover",
            backgroundPosition: "center",
            height: "100vh",
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

              <Button
                endIcon={<LogoutIcon />}
                sx={{
                  color: "white",
                  ml: 4,
                  border: "1px solid white",
                  px: 2,
                }}
                onClick={() => {
                  localStorage.setItem("accessToken", "");
                  router.push("/");
                }}
              >
                {t("Logout")}
              </Button>
            </Box>
          </Box>

          <Box sx={{ maxHeight: "80vh", pt: 8, overflow: "scroll" }}>
            <Box
              sx={{
                display: "flex",
                gap: "24px",
                marginLeft: "100px",
                marginBottom: "24px",
              }}
            >
              <Select
                value={reservationsSearchParams.sortBy}
                onChange={(e) => {
                  queryClient.invalidateQueries({
                    queryKey: [queryKeys.users],
                  });
                  setReservationsSearchParams({
                    ...reservationsSearchParams,
                    sortBy: e.target.value,
                  });
                }}
                sx={{
                  width: "250px",
                  color: "black",
                  "& .MuiSvgIcon-root": {
                    color: "black",
                  },
                  "& .MuiInputBase-input": {
                    color: "black",
                  },
                  backgroundColor: "rgba(255, 255, 255, 0.8)",
                }}
              >
                {sortByOptions.map((option) => (
                  <MenuItem key={option} value={option}>
                    {generateSortByOptionsLabel(option)}
                  </MenuItem>
                ))}
              </Select>
              <Select
                value={reservationsSearchParams.sortDirection}
                onChange={(e) => {
                  queryClient.invalidateQueries({
                    queryKey: [queryKeys.users],
                  });
                  setReservationsSearchParams({
                    ...reservationsSearchParams,
                    sortDirection: e.target.value,
                  });
                }}
                sx={{
                  color: "black",
                  "& .MuiSvgIcon-root": {
                    color: "black",
                  },
                  "& .MuiInputBase-input": {
                    color: "black",
                  },
                  backgroundColor: "rgba(255, 255, 255, 0.8)",
                }}
              >
                <MenuItem key={"Ascending"} value={"Ascending"}>
                  {t("Ascending")}
                </MenuItem>
                <MenuItem key={"Descending"} value={"Descending"}>
                  {t("Descending")}
                </MenuItem>
              </Select>
            </Box>

            {userReservations.items.length > 0 && (
              <Grid container spacing={10}>
                {userReservations.items.map((reservation) => (
                  <Grid
                    item
                    xs={12}
                    sm={12}
                    md={6}
                    lg={6}
                    sx={{ display: "flex", justifyContent: "center" }}
                    key={reservation.id}
                  >
                    <ReservedCar reservation={reservation} />
                  </Grid>
                ))}
              </Grid>
            )}
            {userReservations.items.length === 0 && (
              <Box
                sx={{
                  width: "100%",
                  display: "flex",
                  justifyContent: "center",
                  pt: 20,
                }}
              >
                {!isUserReservationsFetching && (
                  <Typography variant="h3">
                    {t("No reservations found")}
                  </Typography>
                )}
                {isUserReservationsFetching && (
                  <Typography variant="h3">Loading...</Typography>
                )}
              </Box>
            )}
          </Box>
        </Box>
      )}
    </>
  );
}
