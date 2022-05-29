// @flow
import * as React from 'react';
import {FixtureDto} from "../../app/models/football/fixtures";
import {Box, Grid} from "@mui/material";
import {
    fixtureDetailsDataSectionScoreOuterBoxStyle,
    fixtureDetailsDataSectionScoreTeamLogoBoxStyle,
    fixtureDetailsDataSectionScoreTeamLogoGridItemStyle, fixtureDetailsDataSectionScoreTeamNameGridItemStyle
} from "../../styles/features/fixtureDetails/fixtureDetailsDataSectionStyle";
import {useEffect, useState} from "react";
import {TeamDto} from "../../app/models/football/teams";
import agent from "../../app/api/agent";
import {ScoreDto} from "../../app/models/football/scores";

type Props = {
    fixture?: FixtureDto
};
export const FixtureDetailsDataSectionScore = ({fixture}: Props) => {

    const [homeTeam, setHomeTeam] = useState<TeamDto|undefined>(undefined);
    const [awayTeam, setAwayTeam] = useState<TeamDto|undefined>(undefined);
    const [score, setScore] = useState<ScoreDto|undefined>(undefined);

    useEffect(() => {
        if(fixture && fixture.homeTeamId)
        {
            agent.Teams.getById(fixture.homeTeamId)
                .then(res => {
                    setHomeTeam(res.data)
                })
        }
    }, [fixture, fixture?.homeTeamId])

    useEffect(() => {
        if(fixture && fixture.awayTeamId)
        {
            agent.Teams.getById(fixture.awayTeamId)
                .then(res => {
                    setAwayTeam(res.data)
                })
        }
    }, [fixture, fixture?.awayTeamId])

    useEffect(() => {
        if(fixture && fixture.scoreId)
        {
            agent.Scores.getById(fixture.scoreId)
                .then(res => {
                    setScore(res.data)
                })
        }
    }, [fixture, fixture?.scoreId])

    return (
        <Box sx={fixtureDetailsDataSectionScoreOuterBoxStyle}>
            <Grid container>
                <Grid item xs={2} sx={{...fixtureDetailsDataSectionScoreTeamLogoGridItemStyle, justifyContent: "right"}}>
                    <Box
                        component="img"
                        alt="home team logo"
                        src={homeTeam?.logo || "/images/placeholders/TeamLogoUnavailablePlaceholder.png"}
                        sx={fixtureDetailsDataSectionScoreTeamLogoBoxStyle}
                    />
                </Grid>
                <Grid item xs={2.5} sx={fixtureDetailsDataSectionScoreTeamNameGridItemStyle}>
                    {homeTeam?.name || "TBD"}
                </Grid>
                <Grid item xs={1} sx={fixtureDetailsDataSectionScoreTeamNameGridItemStyle}>
                    {score?.homeCurrentScore || 0}
                </Grid>
                <Grid item xs={1} sx={fixtureDetailsDataSectionScoreTeamNameGridItemStyle}>
                    VS
                </Grid>
                <Grid item xs={1} sx={fixtureDetailsDataSectionScoreTeamNameGridItemStyle}>
                    {score?.awayCurrentScore || 0}
                </Grid>
                <Grid item xs={2.5} sx={fixtureDetailsDataSectionScoreTeamNameGridItemStyle}>
                    {awayTeam?.name || "TBD"}
                </Grid>
                <Grid item xs={2} sx={{...fixtureDetailsDataSectionScoreTeamLogoGridItemStyle, justifyContent: "left"}}>
                    <Box
                        component="img"
                        alt="home team logo"
                        src={awayTeam?.logo || "/images/placeholders/TeamLogoUnavailablePlaceholder.png"}
                        sx={fixtureDetailsDataSectionScoreTeamLogoBoxStyle}
                    />
                </Grid>
            </Grid>
        </Box>
    );
};