import { Box, Button, Snackbar, TextField, Typography } from "@mui/material";
import { useFormik } from "formik";
import React, { FC, useState } from "react";
import { InputStyle } from "@/styles/input-style";
import { DisabledButtonStyle } from "@/styles/input-style";
import { useTranslation } from "react-i18next";
import { useLogin } from "@/api/users/use-login";
import { LoginRequest } from "@/types/api/users/login-request";

interface LoginProps {
  setRegister: () => void;
  setNone: () => void;
}

export const Login: FC<LoginProps> = (props) => {
  const { setRegister, setNone } = props;
  const { t } = useTranslation();
  const login = useLogin(setNone);

  const initialValues: LoginRequest = {
    email: "test@test.com",
    password: "@Haslo123",
  };

  const formik = useFormik({
    initialValues,
    onSubmit: (values) => {
      login({
        email: values.email,
        password: values.password,
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
          {t("Login form")}
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
          sx={InputStyle}
          name="password"
          value={formik.values.password}
          onChange={formik.handleChange}
        />

        <Button variant="contained" type="submit" sx={DisabledButtonStyle}>
          {t("Login")}
        </Button>
        <Button
          sx={{
            color: "white",
          }}
          onClick={setRegister}
        >
          {t("Don't have account? Register")}
        </Button>
      </Box>
    </form>
  );
};
