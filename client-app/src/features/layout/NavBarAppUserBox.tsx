// @flow 
import * as React from 'react';
import {Box, Button, ButtonGroup} from "@mui/material";
import {
    navBarAppUserBoxStyle,
    navBarAppUserButtonGroupStyle,
    navBarAppUserButtonStyle
} from "../../styles/features/layout/navBarAppUserBoxStyle";
import {Link, NavLink} from "react-router-dom";
import {useStore} from "../../app/stores/store";
import {observer} from "mobx-react-lite";

type Props = {
    
};
export const NavBarAppUserBox = observer((props: Props) => {

    const store = useStore();

    if(store.identityStore.user)
    {
        return (
            <Box sx={navBarAppUserBoxStyle}>
                {store.identityStore.user.userName} - {store.identityStore.user.balance}$
                <Button
                    onClick={store.identityStore.logout}
                    color="secondary"
                >
                    Logout
                </Button>
            </Box>
        )
    }

    return (
        <Box sx={navBarAppUserBoxStyle}>
            <ButtonGroup
                variant="text"
                color="secondary"
                sx={navBarAppUserButtonGroupStyle}
            >
                <Button
                    disableRipple
                    component={Link}
                    to={"/login"}
                    sx={navBarAppUserButtonStyle}
                    id="navbarLoginButton"
                >
                    Login
                </Button>
                <Button
                    disableRipple
                    component={Link}
                    to={"/register"}
                    sx={navBarAppUserButtonStyle}
                    id="navbarRegisterButton"
                >
                    Register
                </Button>
            </ButtonGroup>
        </Box>
    )
})