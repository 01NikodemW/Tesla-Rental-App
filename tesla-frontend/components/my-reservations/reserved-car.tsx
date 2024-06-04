import { Reservation } from "@/types/reservation";
import { Box, Typography } from "@mui/material";
import React, { FC } from "react";

interface ReservedCarProps {
  reservation: Reservation;
}

export const ReservedCar: FC<ReservedCarProps> = ({ reservation }) => {
  console.log("reservation", reservation);

  return (
    <Box
      sx={{
        border: "2px solid white",
        borderRadius: "10px",
        width: "500px",
      }}
    >
      <Box
        sx={{
          backgroundImage: `url(${reservation.car.imageUrl})`,
          backgroundSize: "cover",
          backgroundPosition: "center",
          width: "100%",
          height: "200px",
          borderTopLeftRadius: "8px",
          borderTopRightRadius: "8px",
        }}
      />
      <Box
        sx={{
          bgcolor: "white",
          opacity: 0.8,
          borderBottomLeftRadius: "8px",
          borderBottomRightRadius: "8px",
          display: "flex",
          flexDirection: "column",
          pb: 2,
        }}
      >
        <Box
          sx={{
            display: "flex",
            justifyContent: "space-around",
            py: 2,
          }}
        >
          <Typography variant="h5" sx={{ color: "black" }}>
            Tesla {reservation.car.model === "_3" ? "3" : reservation.car.model}
          </Typography>
          <Typography variant="h5" sx={{ color: "black" }}>
            {reservation.totalPrice}$
          </Typography>
        </Box>

        <Box
          sx={{
            display: "flex",
            justifyContent: "space-around",
            py: 2,
          }}
        >
          <Box>
            <Typography variant="h5" sx={{ color: "black" }}>
              {reservation.rentalLocation.name}
            </Typography>
            <Typography sx={{ color: "black", fontSize: "1rem" }}>
              {new Date(reservation.rentalDate)
                .toLocaleDateString("en-GB")
                .split("/")
                .join(".")}
            </Typography>
          </Box>
          <Box>
            <Typography variant="h5" sx={{ color: "black" }}>
              {reservation.returnLocation.name}
            </Typography>
            <Typography sx={{ color: "black", fontSize: "1rem" }}>
              {new Date(reservation.returnDate)
                .toLocaleDateString("en-GB")
                .split("/")
                .join(".")}
            </Typography>
          </Box>
        </Box>
      </Box>
    </Box>
  );
};
