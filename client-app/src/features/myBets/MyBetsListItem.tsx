// @flow
import * as React from 'react';
import {BaseBetDto} from "../../app/models/bets/bets";
import {Box, Grid} from "@mui/material";
import {useNavigate} from "react-router-dom";
import dayjs from "dayjs";

type Props = {
    bet: BaseBetDto
};
export const MyBetsListItem = ({bet}: Props) => {

    const navigate = useNavigate()

    return (
        <Grid
            className={bet.type.split(":")[0] + "-bet-item"}
            item
            xs={4}
            sx={{px: "2rem", boxSizing: "border-box", cursor: "pointer"}}
            onClick={() => navigate(`/fixtures/${bet.fixtureId}`)}
        >
            <Box
                sx={{width: "100%", fontSize: "3rem", color: "primary.main", fontWeight: "700"}}
            >
                {bet.homeTeamName} - {bet.awayTeamName}
            </Box>
            <Box
                sx={{
                    width: "100%",
                    fontSize: "2rem",
                    color: "primary.main",
                    borderBottom: "5px solid",
                    borderColor: "secondary.main"
                }}
            >
                Your bet: {bet.description}
                <br/>
                Bet value: {bet.value}
                <br/>
                Placed: {dayjs(bet.timeStamp).toString()}
                <br/>
                Status: {bet.status}
            </Box>
        </Grid>
    );
};