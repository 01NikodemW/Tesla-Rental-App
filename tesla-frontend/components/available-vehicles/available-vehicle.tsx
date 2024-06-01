import { Vehicle } from "@/types/vehicle";
import {
  Autocomplete,
  Box,
  Button,
  Snackbar,
  TextField,
  Typography,
} from "@mui/material";
import React, { FC, useState } from "react";
import { Location } from "@/types/location";
import { useRouter } from "next/router";

interface AvailableVehicleProps {
  vehicle: Vehicle;
  locations: Location[];
  rentalDate: Date;
  returnDate: Date;
}

export const AvailableVehicle: FC<AvailableVehicleProps> = (props) => {
  const { vehicle, locations, rentalDate, returnDate } = props;
  const [openRentSuccess, setOpenRentSuccess] = useState(false);
  const [message, setMessage] = useState("");
  const router = useRouter();

  const [selectedRentalLocation, setSelectedRentalLocation] =
    useState<Location | null>(null);
  const [selectedReturnLocation, setSelectedReturnLocation] =
    useState<Location | null>(null);

  const handleRentalLocationChange = (
    event: any,
    newValue: Location | null
  ) => {
    setSelectedRentalLocation(newValue);
  };

  const handleReturnLocationChange = (
    event: any,
    newValue: Location | null
  ) => {
    setSelectedReturnLocation(newValue);
  };

  async function rentVehicle() {
    const response = await fetch(
      `http://localhost:5070/Reservation/ReserveVehicle`,
      {
        method: "POST",
        mode: "cors",
        headers: {
          "Content-Type": "application/json",
          "Access-Control-Allow-Origin": "http://localhost:3000",
          Authorization: "Bearer " + localStorage.getItem("token"),
        },
        body: JSON.stringify({
          vehicleId: vehicle.id,
          rentalLocationId: selectedRentalLocation?.id,
          returnLocationId: selectedReturnLocation?.id,
          rentalDate: rentalDate,
          returnDate: returnDate,
        }),
      }
    );
    if (!response.ok) {
      return;
    }
    setOpenRentSuccess(true);
    setMessage("Tesla was successfully rented!");
  }

  const isButtonDisabled =
    selectedRentalLocation === null || selectedReturnLocation === null;

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
          backgroundImage: `url(${vehicle.imageUrl})`,
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
            Tesla {vehicle.model === "_3" ? "3" : vehicle.model}
          </Typography>
          <Typography variant="h5" sx={{ color: "black" }}>
            {vehicle.rentalPricePerDay}$/day
          </Typography>
        </Box>

        <Box
          sx={{
            display: "flex",
            justifyContent: "space-around",
            py: 2,
          }}
        >
          <Autocomplete
            value={selectedRentalLocation}
            onChange={handleRentalLocationChange}
            options={locations}
            getOptionLabel={(option) => option.name}
            renderInput={(params) => (
              <TextField {...params} label="Rental location" />
            )}
            sx={{
              width: "40%",
              "& .MuiOutlinedInput-root": {
                "& fieldset": {
                  borderColor: "#000",
                },
                "&:hover fieldset": {
                  borderColor: "#000",
                },
                "&.Mui-focused fieldset": {
                  borderColor: "#000",
                },
              },
              "& .MuiOutlinedInput-input": {
                color: "#000",
              },
              "& .MuiInputLabel-root": {
                color: "#000",
              },
              mb: 2,
            }}
          />
          <Autocomplete
            value={selectedReturnLocation}
            onChange={handleReturnLocationChange}
            options={locations}
            getOptionLabel={(option) => option.name}
            renderInput={(params) => (
              <TextField {...params} label="Return location" />
            )}
            sx={{
              width: "40%",
              "& .MuiOutlinedInput-root": {
                "& fieldset": {
                  borderColor: "#000",
                },
                "&:hover fieldset": {
                  borderColor: "#000",
                },
                "&.Mui-focused fieldset": {
                  borderColor: "#000",
                },
              },
              "& .MuiOutlinedInput-input": {
                color: "#000",
              },
              "& .MuiInputLabel-root": {
                color: "#000",
              },
              mb: 2,
            }}
          />
        </Box>
        <Box sx={{ display: "flex", justifyContent: "end" }}>
          <Button
            sx={{
              mr: 3,
              width: "100px",
              color: "black",
              border: "1px solid black",
            }}
            disabled={isButtonDisabled}
            onClick={rentVehicle}
          >
            Rent
          </Button>
        </Box>
      </Box>
      <Snackbar
        open={openRentSuccess}
        autoHideDuration={3000}
        onClose={() => {
          setOpenRentSuccess(false);
          setMessage("");
          router.push("/");
        }}
        message={message}
      />
    </Box>
  );
};
