// @flow
import * as React from 'react';
import {BaseBetDto} from "../../app/models/bets/bets";
import {Box} from "@mui/material";
import {useNavigate} from "react-router-dom";

type Props = {
    bet: BaseBetDto
};
export const MyBetsListItem = ({bet}: Props) => {

    const navigate = useNavigate()

    return (
        <Box
            sx={{width: "100%", height: "10rem", borderBottom: "1px solid", borderColor: "primary.main"}}
            onClick={() => navigate(`/fixtures/${bet.fixtureId}`)}
        >
            {bet.homeTeamName} - {bet.awayTeamName}
            <br/>
            {bet.description}
            <br/>
            {bet.value}$
        </Box>
    );
};