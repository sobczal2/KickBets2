// @flow
import * as React from 'react';
import {Button} from "@mui/material";
import {NavLink} from "react-router-dom";
import {navBarNavigationButtonStyle} from "../../styles/features/layout/navBarNavigationButtonStyle";

type Props = {
    to: string
    content: string
};
export const NavBarNavigationButton = ({to, content}: Props) => {
    return (
        <Button
            component={NavLink}
            to={to}
            disableRipple
            sx={navBarNavigationButtonStyle}
        >
            {content}
        </Button>
    );
};