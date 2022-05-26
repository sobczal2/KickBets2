// @flow
import * as React from 'react';
import {Box} from "@mui/material";
import {NavBarNavigationButton} from "./NavBarNavigationButton";
import {navBarNavigationButtonsBoxStyle} from "../../styles/features/layout/navBarNavigationButtonsBoxStyle";

type Props = {

};
export const NavBarNavigationButtonsBox = (props: Props) => {
    return (
        <Box sx={navBarNavigationButtonsBoxStyle}>
            <NavBarNavigationButton to="/fixtures" content="Dashboard"/>
            <NavBarNavigationButton to="/mybets" content="My Bets"/>
            <NavBarNavigationButton to="/about" content="About"/>
        </Box>
    );
};