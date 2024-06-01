/*
 * Ems.WebApp.Front
 *
 * (c) 2022 Ejsak Gorup
 */
import { Email } from "@mui/icons-material";
import i18n from "i18next";
import { initReactI18next } from "react-i18next";

const resources = {
  en: {
    translation: {},
  },
  pl: {
    translation: {
      Login: "Zaloguj się",
      Logout: "Wyloguj się",
      "Show my rentals": "Pokaż moje wypożyczenia",
      Register: "Zarejestruj się",
      "Check available Tesla cars": "Sprawdź dostępne samochody Tesla",
      "Don't have account? Register": "Nie masz konta? Zarejestruj się",
      "Invalid email or password": "Nieprawidłowy email lub hasło",
      "Login form": "Logowanie",
      "Register form": "Rejestracja",
      Password: "Hasło",
      "Confirm password": "Potwierdź hasło",
      "First Name": "Imię",
      "Last Name": "Nazwisko",
      Country: "Kraj",
      "Email is taken": "Email jest zajęty",
      "Passwords do not match": "Hasła nie pasują",
      "Already have an account? Login": "Masz już konto? Zaloguj się",
      "Welcome back!": "Witaj ponownie!",

      Austria: "Austria",
      Belgium: "Belgia",
      Bulgaria: "Bułgaria",
      Croatia: "Chorwacja",
      Cyprus: "Cypr",
      "Czech Republic": "Czechy",
      Denmark: "Dania",
      Estonia: "Estonia",
      Finland: "Finlandia",
      France: "Francja",
      Germany: "Niemcy",
      Greece: "Grecja",
      Hungary: "Węgry",
      Ireland: "Irlandia",
      Italy: "Włochy",
      Latvia: "Łotwa",
      Lithuania: "Litwa",
      Luxembourg: "Luksemburg",
      Malta: "Malta",
      Netherlands: "Holandia",
      Poland: "Polska",
      Portugal: "Portugalia",
      Romania: "Rumunia",
      Slovakia: "Słowacja",
      Slovenia: "Słowenia",
      Spain: "Hiszpania",
      Sweden: "Szwecja",
    },
  },
};

i18n.use(initReactI18next).init({
  resources,
  lng: "pl",
  fallbackLng: "en",
  interpolation: {
    escapeValue: false,
  },
});

i18n.language = "pl";
