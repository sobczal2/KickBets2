// @flow
import * as React from 'react';
import {AppBar, Box, Toolbar, Typography} from "@mui/material";
import {
    navBarBoxStyle,
    navBarMainTitleStyle,
    navBarStyle
} from "../../styles/app/layout/navBarStyle";
import {useNavigate} from "react-router-dom";
import {NavBarNavigationButtonsBox} from "../../features/layout/NavBarNavigationButtonsBox";
import {NavBarAppUserBox} from "../../features/layout/NavBarAppUserBox";

type Props = {

};
export const NavBar = (props: Props) => {

    const navigate = useNavigate();

    return (
            <AppBar position="sticky" sx={navBarStyle}>
                <Box sx={navBarBoxStyle}>
                    <Toolbar disableGutters sx={{py: "2rem"}}>
                        <Typography
                            sx={navBarMainTitleStyle}
                            onClick={() => navigate("/")}
                        >
                            KICKBETS
                        </Typography>
                        <NavBarNavigationButtonsBox/>
                        <NavBarAppUserBox />
                    </Toolbar>
                </Box>
            </AppBar>
    );
};