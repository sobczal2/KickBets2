// @flow
import * as React from 'react';
import {Box, Grid, Typography} from "@mui/material";
import {
    fixtureDetailsBetsSectionDisplayHeaderStyle,
    fixtureDetailsBetsSectionDisplayInnerBoxStyle,
    fixtureDetailsBetsSectionDisplayMultiplierStyle,
    fixtureDetailsBetsSectionDisplayOuterBoxStyle,
    fixtureDetailsBetsSectionDisplayTitleStyle
} from "../../styles/features/fixtureDetails/fixtureDetailsBetsSectionStyle";
import {FixtureDto} from "../../app/models/football/fixtures";
import {useEffect, useState} from "react";
import {WdlhtBetsDataDto} from "../../app/models/bets/wdlhtBets";
import agent from "../../app/api/agent";
import dayjs from "dayjs";

type Props = {
    fixture?: FixtureDto
};
export const FixtureDetailsBetsSectionWdlhtDisplay = ({fixture}: Props) => {

    const [wdlhtData, setWdlhtData] = useState<WdlhtBetsDataDto | undefined>(undefined)
    const [homeTeamName, setHomeTeamName] = useState<string | undefined>(undefined)
    const [awayTeamName, setAwayTeamName] = useState<string | undefined>(undefined)

    const available = dayjs().isBefore(fixture?.date)

    useEffect(() => {
        if (fixture && fixture.betsDataId) {
            agent.Bets.getBetsData(fixture.betsDataId)
                .then(res => {
                    setWdlhtData(res.data.wdlhtBetsData)
                })
        }
    }, [fixture, fixture?.betsDataId])

    useEffect(() => {
        if (fixture && fixture.homeTeamId) {
            agent.Teams.getById(fixture.homeTeamId)
                .then(res => {
                    setHomeTeamName(res.data.name)
                })
        }
    }, [fixture, fixture?.homeTeamId])

    useEffect(() => {
        if (fixture && fixture.awayTeamId) {
            agent.Teams.getById(fixture.awayTeamId)
                .then(res => {
                    setAwayTeamName(res.data.name)
                })
        }
    }, [fixture, fixture?.awayTeamId])

    return (
        <Box
            sx={{...fixtureDetailsBetsSectionDisplayOuterBoxStyle, opacity: available ? "100%" : "50%"}}
            onClick={() => console.log("siema")}
        >
            <Box sx={fixtureDetailsBetsSectionDisplayHeaderStyle}>
                Bet: Score at half time
            </Box>
            <Grid container>
                <Grid item xs={4}>
                    <Box sx={fixtureDetailsBetsSectionDisplayInnerBoxStyle}>
                        <Typography
                            sx={fixtureDetailsBetsSectionDisplayTitleStyle}
                        >
                            {homeTeamName} to win at half time
                        </Typography>
                        <Typography
                            sx={fixtureDetailsBetsSectionDisplayMultiplierStyle}
                        >
                        {wdlhtData?.homeBetsMultiplier ? `x${wdlhtData?.homeBetsMultiplier.toFixed(2)}` : "unknown"}
                        </Typography>
                    </Box>
                </Grid>
                <Grid item xs={4}>
                    <Box sx={fixtureDetailsBetsSectionDisplayInnerBoxStyle}>
                        <Typography
                            sx={fixtureDetailsBetsSectionDisplayTitleStyle}
                        >
                            Draw at half time
                        </Typography>
                        <Typography
                            sx={fixtureDetailsBetsSectionDisplayMultiplierStyle}
                        >
                        {wdlhtData?.drawBetsMultiplier ? `x${wdlhtData?.homeBetsMultiplier.toFixed(2)}` : "unknown"}
                        </Typography>
                    </Box>
                </Grid>
                <Grid item xs={4}>
                    <Box sx={{...fixtureDetailsBetsSectionDisplayInnerBoxStyle, border: "none"}}>
                        <Typography
                            sx={fixtureDetailsBetsSectionDisplayTitleStyle}
                        >
                            {awayTeamName} to win at half time
                        </Typography>
                        <Typography
                            sx={fixtureDetailsBetsSectionDisplayMultiplierStyle}
                        >
                            {wdlhtData?.awayBetsMultiplier ? `x${wdlhtData?.homeBetsMultiplier.toFixed(2)}` : "unknown"}
                        </Typography>
                    </Box>
                </Grid>
            </Grid>
        </Box>
    );
};