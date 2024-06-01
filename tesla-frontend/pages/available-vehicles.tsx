import { Box, Button, Grid, Typography } from "@mui/material";
import LocationOnIcon from "@mui/icons-material/LocationOn";
import LocalPhoneIcon from "@mui/icons-material/LocalPhone";
import { useRouter } from "next/router";
import { AvailableVehicle } from "@/components/available-vehicles/available-vehicle";
import LogoutIcon from "@mui/icons-material/Logout";
import { useAvailableCars } from "@/api/cars/use-available-cars";

export default function AvailableVehicles() {
  const router = useRouter();
  const rentalDate =
    typeof router.query.rentalDate === "string" ? router.query.rentalDate : "";

  const returnDate =
    typeof router.query.returnDate === "string" ? router.query.returnDate : "";

  const { availableCars, isAvailableCarsFetching } = useAvailableCars({
    rentalDate: rentalDate,
    returnDate: returnDate,
  });

  return (
    <>
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
          <Button
            onClick={() => {
              router.push("/my-reservations");
            }}
          >
            Show my rentals
          </Button>
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
              sx={{ color: "white", ml: 4, border: "1px solid white", px: 2 }}
              onClick={() => {
                localStorage.removeItem("accessToken");
                router.push("/");
              }}
            >
              Logout
            </Button>
          </Box>
        </Box>
        <Box sx={{ maxHeight: "80vh", pt: 8, overflow: "scroll" }}>
          {availableCars.length > 0 && (
            <Grid container spacing={5}>
              {availableCars.map((car) => (
                <Grid
                  item
                  xs={12}
                  sm={12}
                  md={6}
                  lg={6}
                  sx={{ display: "flex", justifyContent: "center" }}
                  key={car.id}
                >
                  <AvailableVehicle
                    car={car}
                    rentalDate={rentalDate}
                    returnDate={returnDate}
                  />
                </Grid>
              ))}
            </Grid>
          )}
          {availableCars.length === 0 && (
            <Box
              sx={{
                width: "100%",
                display: "flex",
                justifyContent: "center",
                pt: 20,
              }}
            >
              {!isAvailableCarsFetching && (
                <Typography variant="h3">
                  No available Tesla was found
                </Typography>
              )}
              {isAvailableCarsFetching && (
                <Typography variant="h3">Loading...</Typography>
              )}
            </Box>
          )}
        </Box>
      </Box>
    </>
  );
}
