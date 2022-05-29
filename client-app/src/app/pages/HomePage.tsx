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
                <span className="l1">K</span>
                <span className="l2">i</span>
                <span className="l3">c</span>
                <span className="l4">k</span>
                <span className="l5">B</span>
                <span className="l6">e</span>
                <span className="l7">t</span>
                <span className="l8">s</span>
            </Typography>
            <Button sx={homePageButtonStyle} onClick={() => navigate("/fixtures")} disableRipple>
                Get started!
            </Button>
        </Box>
    );
};