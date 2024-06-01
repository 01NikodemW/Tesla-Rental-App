import { Box, Button, Grid, Typography } from "@mui/material";
import LocationOnIcon from "@mui/icons-material/LocationOn";
import LocalPhoneIcon from "@mui/icons-material/LocalPhone";
import { useEffect, useState } from "react";
import { useRouter } from "next/router";
import { AvailableVehicle } from "@/components/available-vehicles/available-vehicle";
import { Vehicle } from "@/types/vehicle";
import { Location } from "@/types/location";
import LogoutIcon from "@mui/icons-material/Logout";

export default function AvailableVehicles() {
  const router = useRouter();
  const [availableVehicles, setAvailableVehicles] = useState<Vehicle[]>([]);
  const [locations, setLocations] = useState<Location[]>([]);
  const [rentalDate, setRentalDate] = useState<Date>(new Date());
  const [returnDate, setReturnDate] = useState<Date>(new Date());
  const [isLoading, setIsLoading] = useState<boolean>(true);

  useEffect(() => {
    async function GetAvailableVehicles() {
      const rentalDate = localStorage.getItem("rentalDate");
      const returnDate = localStorage.getItem("returnDate");

      if (!rentalDate || !returnDate) {
        return;
      }

      setRentalDate(new Date(rentalDate));
      setReturnDate(new Date(returnDate));

      const dataRange = {
        RentalDate: new Date(rentalDate),
        ReturnDate: new Date(returnDate),
      };
      const response = await fetch(
        `http://localhost:5070/Vehicle/GetAvailableVehicles`,
        {
          method: "POST",
          mode: "cors",
          headers: {
            "Content-Type": "application/json",
            "Access-Control-Allow-Origin": "http://localhost:3000",
          },
          body: JSON.stringify(dataRange),
        }
      );
      if (!response.ok) {
        setIsLoading(false);

        return;
      }
      const data = await response.json();
      setAvailableVehicles(data);
      setIsLoading(false);
    }

    async function GetLocations() {
      const response = await fetch("http://localhost:5070/Location", {
        method: "GET",
        mode: "cors",
        headers: {
          "Content-Type": "application/json",
          "Access-Control-Allow-Origin": "http://localhost:3000",
        },
      });
      if (!response.ok) {
        setIsLoading(false);

        return;
      }
      const data = await response.json();
      setLocations(data);
      setIsLoading(false);
    }

    if (!localStorage.getItem("token")) {
      router.push("/");
    } else {
      GetAvailableVehicles();
      GetLocations();
    }
  }, []);

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
                localStorage.removeItem("token");
                router.push("/");
              }}
            >
              Logout
            </Button>
          </Box>
        </Box>
        <Box sx={{ maxHeight: "80vh", pt: 8, overflow: "scroll" }}>
          {availableVehicles.length > 0 && (
            <Grid container spacing={5}>
              {availableVehicles.map((vehicle) => (
                <Grid
                  item
                  xs={12}
                  sm={12}
                  md={6}
                  lg={6}
                  sx={{ display: "flex", justifyContent: "center" }}
                  key={vehicle.id}
                >
                  <AvailableVehicle
                    vehicle={vehicle}
                    locations={locations}
                    rentalDate={rentalDate}
                    returnDate={returnDate}
                  />
                </Grid>
              ))}
            </Grid>
          )}
          {availableVehicles.length === 0 && (
            <Box
              sx={{
                width: "100%",
                display: "flex",
                justifyContent: "center",
                pt: 20,
              }}
            >
              {!isLoading && (
                <Typography variant="h3">
                  No available Tesla was found
                </Typography>
              )}
              {isLoading && <Typography variant="h3">Loading...</Typography>}
            </Box>
          )}
        </Box>
      </Box>
    </>
  );
}
