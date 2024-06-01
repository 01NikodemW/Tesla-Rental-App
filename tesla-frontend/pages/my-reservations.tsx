import { Box, Button, Grid, Typography } from "@mui/material";
import LocationOnIcon from "@mui/icons-material/LocationOn";
import LocalPhoneIcon from "@mui/icons-material/LocalPhone";
import { useEffect, useState } from "react";
import { useRouter } from "next/router";
import LogoutIcon from "@mui/icons-material/Logout";
import { useUserReservations } from "@/api/reservations/use-user-reservations";
import { ReservedCar } from "@/components/my-reservations/reserved-car";

export default function MyReservations() {
  const router = useRouter();
  const [isAuthenticated, setIsAuthenticated] = useState<boolean>(false);

  const { userReservations, isUserReservationsFetching } =
    useUserReservations();

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
                Logout
              </Button>
            </Box>
          </Box>
          <Box sx={{ maxHeight: "80vh", pt: 8, overflow: "scroll" }}>
            {userReservations.length > 0 && (
              <Grid container spacing={10}>
                {userReservations.map((reservation) => (
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
            {userReservations.length === 0 && (
              <Box
                sx={{
                  width: "100%",
                  display: "flex",
                  justifyContent: "center",
                  pt: 20,
                }}
              >
                {!isUserReservationsFetching && (
                  <Typography variant="h3">No reservations found</Typography>
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
