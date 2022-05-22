import {createTheme} from "@mui/material";

export default createTheme({
    palette:
        {
            primary:
                {
                    main: "#303030",
                    dark: "#000000",
                    light: "#a0a0a0",
                },
            secondary:
                {
                    main: "#fdee30",
                    dark: "#5e5813",
                    light: "#faeb14"
                },
            background: {
                default: "#fefefe",
                paper: "#c0c0c0"
            },
            text:
                {
                    primary: "#000000",
                    secondary: "#ffffff",
                    disabled: "#555555",
                }
        }
})