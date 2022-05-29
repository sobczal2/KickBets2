// @flow 
import * as React from 'react';
import {FixtureDto} from "../../app/models/football/fixtures";
import {Box, CircularProgress, Grid, Typography} from "@mui/material";
import {
    fixtureDetailsDataSectionStatisticsTabOuterBoxStyle
} from "../../styles/features/fixtureDetails/fixtureDetailsDataSectionStatisticsTabStyle";
import {useEffect, useState} from "react";
import {LineupDto} from "../../app/models/football/lineups";
import agent from "../../app/api/agent";
import {PlayerDto} from "../../app/models/football/players";
import {
    fixtureDetailsDataSectionLineupsTabOuterBoxStyle,
    fixtureDetailsDataSectionLineupsTabPlayerGridItemStyle,
    fixtureDetailsDataSectionLineupsTabPlayersHeaderStyle
} from "../../styles/features/fixtureDetails/fixtureDetailsDataSectionLineupsTabStyle";

type Props = {
    fixture?: FixtureDto
};
export const FixtureDetailsDataSectionLineupsTab = ({fixture}: Props) => {

    const [homeLineup, setHomeLineup] = useState<LineupDto | undefined>(undefined)
    const [awayLineup, setAwayLineup] = useState<LineupDto | undefined>(undefined)
    const [homePlayers, setHomePlayers] = useState<PlayerDto[]>([])
    const [awayPlayers, setAwayPlayers] = useState<PlayerDto[]>([])
    const [loading, setLoading] = useState(false)

    useEffect(() => {
        if (!(fixture && fixture.homeLineupId && fixture.awayLineupId)) return
        let fetched = 0
        setLoading(true)
        agent.Lineups.getById(fixture.homeLineupId)
            .then(res => {
                console.log(res.data)
                setHomeLineup(res.data)
            })
            .finally(() => {
                if (fetched === 1)
                    setLoading(false)
                fetched += 1
            })
        agent.Lineups.getById(fixture.awayLineupId)
            .then(res => {
                setAwayLineup(res.data)
            })
            .finally(() => {
                if (fetched === 1)
                    setLoading(false)
                fetched += 1
            })
    }, [fixture, fixture?.homeLineupId, fixture?.awayLineupId])

    useEffect(() => {
        if (!(fixture && fixture.homeLineupId && fixture.awayLineupId)) return
        let fetched = 0
        setLoading(true)
        agent.Lineups.getPlayersByLineupId(fixture.homeLineupId)
            .then(res => {
                console.log(res.data)
                setHomePlayers(res.data)
            })
            .finally(() => {
                if (fetched === 1)
                    setLoading(false)
                fetched += 1
            })
        agent.Lineups.getPlayersByLineupId(fixture.awayLineupId)
            .then(res => {
                setAwayPlayers(res.data)
            })
            .finally(() => {
                if (fetched === 1)
                    setLoading(false)
                fetched += 1
            })
    }, [fixture, fixture?.homeLineupId, fixture?.awayLineupId])

    if (loading) {
        return (
            <Box sx={{
                ...fixtureDetailsDataSectionStatisticsTabOuterBoxStyle,
                display: "flex",
                alignItems: "center",
                justifyContent: "center"
            }}>
                <CircularProgress color="secondary"/>
            </Box>
        )
    }

    if (!homeLineup || !awayLineup) {
        return (
            <Box sx={{
                ...fixtureDetailsDataSectionStatisticsTabOuterBoxStyle,
                display: "flex",
                alignItems: "center",
                justifyContent: "center"
            }}>
                <Typography
                    color="secondary"
                    variant="h2"
                >
                    Not available
                </Typography>
            </Box>
        )
    }

    return (
        <Box sx={fixtureDetailsDataSectionLineupsTabOuterBoxStyle}>
            <Grid container sx={{width: "100%"}}>
                <Grid item xs={6}>
                    <Box sx={{borderRight: "2px solid", borderColor: "secondary.main", height: "100%"}}>
                        <Typography
                            variant="h3"
                            sx={fixtureDetailsDataSectionLineupsTabPlayersHeaderStyle}
                        >
                            Starting eleven:
                        </Typography>
                        <Grid container>
                            <>
                                <Grid item xs={2} sx={fixtureDetailsDataSectionLineupsTabPlayerGridItemStyle}>
                                    Number:
                                </Grid>
                                <Grid item xs={6} sx={fixtureDetailsDataSectionLineupsTabPlayerGridItemStyle}>
                                    Name:
                                </Grid>
                                <Grid item xs={4} sx={{...fixtureDetailsDataSectionLineupsTabPlayerGridItemStyle, mb: "1rem"}}>
                                    Position:
                                </Grid>
                            </>
                            {homePlayers.filter(p => p.starting11).map((p, i) => (
                                <React.Fragment key={i}>
                                    <Grid item xs={2} sx={fixtureDetailsDataSectionLineupsTabPlayerGridItemStyle}>
                                        {p.number}
                                    </Grid>
                                    <Grid item xs={6} sx={fixtureDetailsDataSectionLineupsTabPlayerGridItemStyle}>
                                        {p.name}
                                    </Grid>
                                    <Grid item xs={4} sx={fixtureDetailsDataSectionLineupsTabPlayerGridItemStyle}>
                                        {p.pos}
                                    </Grid>
                                </React.Fragment>
                            ))}
                        </Grid>
                        <Typography
                            variant="h3"
                            sx={fixtureDetailsDataSectionLineupsTabPlayersHeaderStyle}
                        >
                            Substitutes:
                        </Typography>
                        <Grid container>
                            <>
                                <Grid item xs={2} sx={fixtureDetailsDataSectionLineupsTabPlayerGridItemStyle}>
                                    Number:
                                </Grid>
                                <Grid item xs={6} sx={fixtureDetailsDataSectionLineupsTabPlayerGridItemStyle}>
                                    Name:
                                </Grid>
                                <Grid item xs={4} sx={{...fixtureDetailsDataSectionLineupsTabPlayerGridItemStyle, mb: "1rem"}}>
                                    Position:
                                </Grid>
                            </>
                            {homePlayers.filter(p => !p.starting11).map((p, i) => (
                                <React.Fragment key={i}>
                                    <Grid item xs={2} sx={fixtureDetailsDataSectionLineupsTabPlayerGridItemStyle}>
                                        {p.number}
                                    </Grid>
                                    <Grid item xs={6} sx={fixtureDetailsDataSectionLineupsTabPlayerGridItemStyle}>
                                        {p.name}
                                    </Grid>
                                    <Grid item xs={4} sx={fixtureDetailsDataSectionLineupsTabPlayerGridItemStyle}>
                                        {p.pos}
                                    </Grid>
                                </React.Fragment>
                            ))}
                        </Grid>
                        <Typography
                            variant="h3"
                            sx={fixtureDetailsDataSectionLineupsTabPlayersHeaderStyle}
                        >
                            Coach: {homeLineup.coachName}
                        </Typography>
                    </Box>
                </Grid>
                <Grid item xs={6}>
                    <Box sx={{borderLeft: "2px solid", borderColor: "secondary.main", height: "100%"}}>
                        <Typography
                            variant="h3"
                            sx={fixtureDetailsDataSectionLineupsTabPlayersHeaderStyle}
                        >
                            Starting eleven:
                        </Typography>
                        <Grid container>
                            <>
                                <Grid item xs={2} sx={fixtureDetailsDataSectionLineupsTabPlayerGridItemStyle}>
                                    Number:
                                </Grid>
                                <Grid item xs={6} sx={fixtureDetailsDataSectionLineupsTabPlayerGridItemStyle}>
                                    Name:
                                </Grid>
                                <Grid item xs={4} sx={{...fixtureDetailsDataSectionLineupsTabPlayerGridItemStyle, mb: "1rem"}}>
                                    Position:
                                </Grid>
                            </>
                            {awayPlayers.filter(p => p.starting11).map((p, i) => (
                                <>
                                    <Grid item xs={2} sx={fixtureDetailsDataSectionLineupsTabPlayerGridItemStyle}>
                                        {p.number}
                                    </Grid>
                                    <Grid item xs={6} sx={fixtureDetailsDataSectionLineupsTabPlayerGridItemStyle}>
                                        {p.name}
                                    </Grid>
                                    <Grid item xs={4} sx={fixtureDetailsDataSectionLineupsTabPlayerGridItemStyle}>
                                        {p.pos}
                                    </Grid>
                                </>
                            ))}
                        </Grid>
                        <Typography
                            variant="h3"
                            sx={fixtureDetailsDataSectionLineupsTabPlayersHeaderStyle}
                        >
                            Substitutes:
                        </Typography>
                        <Grid container>
                            <>
                                <Grid item xs={2} sx={fixtureDetailsDataSectionLineupsTabPlayerGridItemStyle}>
                                    Number:
                                </Grid>
                                <Grid item xs={6} sx={fixtureDetailsDataSectionLineupsTabPlayerGridItemStyle}>
                                    Name:
                                </Grid>
                                <Grid item xs={4} sx={{...fixtureDetailsDataSectionLineupsTabPlayerGridItemStyle, mb: "1rem"}}>
                                    Position:
                                </Grid>
                            </>
                            {awayPlayers.filter(p => !p.starting11).map((p, i) => (
                                <>
                                    <Grid item xs={2} sx={fixtureDetailsDataSectionLineupsTabPlayerGridItemStyle}>
                                        {p.number}
                                    </Grid>
                                    <Grid item xs={6} sx={fixtureDetailsDataSectionLineupsTabPlayerGridItemStyle}>
                                        {p.name}
                                    </Grid>
                                    <Grid item xs={4} sx={fixtureDetailsDataSectionLineupsTabPlayerGridItemStyle}>
                                        {p.pos}
                                    </Grid>
                                </>
                            ))}
                        </Grid>
                        <Typography
                            variant="h3"
                            sx={fixtureDetailsDataSectionLineupsTabPlayersHeaderStyle}
                        >
                            Coach: {awayLineup.coachName}
                        </Typography>
                    </Box>
                </Grid>
            </Grid>
        </Box>
    )
};