// @flow 
import * as React from 'react';
import {Box, Button, Typography} from "@mui/material";
import {homePageButtonStyle, homePageStyle, homePageTitleStyle} from "../../styles/app/pages/homePageStyle";
import {useNavigate} from "react-router-dom";

type Props = {
    
};
export const HomePage = (props: Props) => {

    const navigate = useNavigate();

    return (
        <Box sx={homePageStyle}>
            <Typography
                variant="h1"
                sx={homePageTitleStyle}
            >
                KickBets
            </Typography>
            <Button sx={homePageButtonStyle} onClick={() => navigate("/fixtures")} disableRipple>
                Get started!
            </Button>
        </Box>
    );
};