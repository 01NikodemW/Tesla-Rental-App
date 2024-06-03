import {
  Autocomplete,
  Box,
  Button,
  TextField,
  Typography,
} from "@mui/material";
import React, { FC, useState } from "react";
import { Location } from "@/types/location";
import { useRouter } from "next/router";
import { Car } from "@/types/car";
import { useLocations } from "@/api/locations/use-locations";
import { useTranslation } from "react-i18next";
import { useReservateCar } from "@/api/reservations/use-reservate";
import toast from "react-hot-toast";

interface AvailableCarProps {
  car: Car;
  rentalDate: string;
  returnDate: string;
}

export const AvailableCar: FC<AvailableCarProps> = ({
  car,
  rentalDate,
  returnDate,
}) => {
  const router = useRouter();
  const { locations, isLocationsFetching } = useLocations();
  const { t } = useTranslation();

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

  const reservateCar = useReservateCar(() => {
    toast.success(t("Car has been reserved"));
    router.push("/my-reservations");
  });

  const handleReservateCar = () => {
    reservateCar({
      carId: car.id,
      rentalLocationId: selectedRentalLocation?.id,
      returnLocationId: selectedReturnLocation?.id,
      rentalDate: rentalDate,
      returnDate: returnDate,
    });
  };

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
          backgroundImage: `url(${car.imageUrl})`,
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
            Tesla {car.model === "_3" ? "3" : car.model}
          </Typography>
          <Typography variant="h5" sx={{ color: "black" }}>
            {`${car.rentalPricePerDay}$/${t("day")}`}
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
            loading={isLocationsFetching}
            getOptionLabel={(option) => option.name}
            renderInput={(params) => (
              <TextField {...params} label={t("Rental location")} />
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
            loading={isLocationsFetching}
            getOptionLabel={(option) => option.name}
            renderInput={(params) => (
              <TextField {...params} label={t("Return location")} />
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
            onClick={handleReservateCar}
          >
            {t("Rent")}
          </Button>
        </Box>
      </Box>
    </Box>
  );
};
