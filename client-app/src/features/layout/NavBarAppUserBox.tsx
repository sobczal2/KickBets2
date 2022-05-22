// @flow 
import * as React from 'react';
import {Box, Button, ButtonGroup} from "@mui/material";
import {
    navBarAppUserBoxStyle,
    navBarAppUserButtonGroupStyle,
    navBarAppUserButtonStyle
} from "../../styles/features/layout/navBarAppUserBoxStyle";

type Props = {
    
};
export const NavBarAppUserBox = (props: Props) => {
    if(true)
    {
        return (
            <Box sx={navBarAppUserBoxStyle}>
                <ButtonGroup
                    variant="text"
                    color="secondary"
                    sx={navBarAppUserButtonGroupStyle}
                >
                    <Button
                        disableRipple
                        sx={navBarAppUserButtonStyle}
                    >
                        Login
                    </Button>
                    <Button
                        disableRipple
                        sx={navBarAppUserButtonStyle}
                    >
                        Register
                    </Button>
                </ButtonGroup>
            </Box>
        )
    }
    return (
        <Box sx={navBarAppUserBoxStyle}>

        </Box>
    );
};