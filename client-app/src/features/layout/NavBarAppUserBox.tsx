// @flow
import * as React from 'react';
import {Box, Button, ButtonGroup} from "@mui/material";
import {
    navBarAppUserBoxStyle,
    navBarAppUserButtonGroupStyle,
    navBarAppUserButtonStyle
} from "../../styles/features/layout/navBarAppUserBoxStyle";
import {Link} from "react-router-dom";
import {useStore} from "../../app/stores/store";
import {observer} from "mobx-react-lite";
import agent from "../../app/api/agent";
import {toast} from "react-toastify";

type Props = {};
export const NavBarAppUserBox = observer((props: Props) => {

    const store = useStore();

    if (store.identityStore.user) {
        return (
            <Box sx={navBarAppUserBoxStyle}>
                {store.identityStore.user.userName} - {store.identityStore.user.balance}$
                <Button
                    id="navbar-add-balance-button"
                    size="large"
                    sx={{fontSize: "1.5rem", ml: "0.5rem"}}
                    onClick={() => {
                        agent.Identity.addBalance()
                            .then(() => {
                                toast("Balance added", {type: "success"})
                                store.identityStore.aboutMe(false)
                            })
                            .catch(err => {
                                toast(err.response.data.Errors.Balance || "Unknown error", {type: "error"})
                            })
                    }}
                    color="secondary"
                >
                    Add balance!
                </Button>
                <Button
                    id="navbar-logout-button"
                    size="large"
                    sx={{fontSize: "1.5rem", ml: "0.5rem"}}
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