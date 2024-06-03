import {
  Autocomplete,
  Box,
  Button,
  TextField,
  Typography,
} from "@mui/material";
import React, { FC, useState } from "react";
import { DisabledButtonStyle, InputStyle } from "@/styles/input-style";
import { useFormik } from "formik";
import { useCountries } from "@/api/countries/use-countries";
import { useTranslation } from "react-i18next";
import { RegisterRequest } from "@/types/api/users/register-request";
import { useRegister } from "@/api/users/use-register";
import { Country } from "@/types/country";

interface RegisterProps {
  setLogin: () => void;
}

export const Register: FC<RegisterProps> = ({ setLogin }) => {
  const [selectedCountry, setSelectedCountry] = useState<Country | null>(null);
  const handleCountryChange = (event: any, newValue: Country | null) => {
    setSelectedCountry(newValue);
  };
  const { t } = useTranslation();
  const register = useRegister(setLogin);

  const { countries, isCountriesFetching } = useCountries();

  const initialValues: RegisterRequest = {
    email: "email@gmail.com",
    password: "@Haslo123",
    passwordConfirmation: "@Haslo123",
    firstName: "Jan",
    lastName: "Nowak",
  };

  const formik = useFormik({
    initialValues,
    onSubmit: (values) => {
      register({
        email: values.email,
        password: values.password,
        passwordConfirmation: values.passwordConfirmation,
        firstName: values.firstName,
        lastName: values.lastName,
      });
    },
  });

  return (
    <form onSubmit={formik.handleSubmit}>
      <Box
        sx={{
          border: "2px solid white",
          display: "flex",
          borderRadius: "10px",
          flexDirection: "column",
          alignItems: "center",
          padding: "20px",
        }}
      >
        <Typography variant="h5" sx={{ mb: 2 }}>
          {t("Register form")}
        </Typography>
        <TextField
          label="Email"
          sx={InputStyle}
          name="email"
          value={formik.values.email}
          onChange={formik.handleChange}
        />
        <TextField
          label={t("Password")}
          type="password"
          name="password"
          sx={InputStyle}
          value={formik.values.password}
          onChange={formik.handleChange}
        />
        <TextField
          label={t("Confirm password")}
          type="password"
          name="passwordConfirmation"
          sx={InputStyle}
          value={formik.values.passwordConfirmation}
          onChange={formik.handleChange}
        />
        <TextField
          label={t("First Name")}
          name="fistName"
          sx={InputStyle}
          value={formik.values.firstName}
          onChange={formik.handleChange}
        />
        <TextField
          label={t("Last Name")}
          name="lastName"
          sx={InputStyle}
          value={formik.values.lastName}
          onChange={formik.handleChange}
        />
        <Autocomplete
          value={selectedCountry}
          onChange={handleCountryChange}
          options={countries}
          getOptionLabel={(option) => t(option.name)}
          renderInput={(params) => (
            <TextField {...params} label={t("Country")} />
          )}
          loading={isCountriesFetching}
          sx={{
            width: "70%",
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
            mb: 2,
          }}
        />

        <Button variant="contained" type="submit" sx={DisabledButtonStyle}>
          {t("Register")}
        </Button>
        <Button
          sx={{
            color: "white",
          }}
          onClick={setLogin}
        >
          {t("Already have an account? Login")}
        </Button>
      </Box>
    </form>
  );
};
