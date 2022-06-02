// @flow
import * as React from 'react';
import {useEffect, useRef, useState} from "react";
import {LineupDto} from "../../app/models/football/lineups";
import {PlayerDto} from "../../app/models/football/players";
import agent from "../../app/api/agent";
import {Box, CircularProgress, Grid, Typography} from "@mui/material";
import {
    fixtureDetailsDataSectionStatisticsTabOuterBoxStyle
} from "../../styles/features/fixtureDetails/fixtureDetailsDataSectionStatisticsTabStyle";
import {FixtureDto} from "../../app/models/football/fixtures";
import useResizeObserver from "@react-hook/resize-observer";
import {
    fixtureDetailsDataSectionFormationTabImageBoxStyle,
    fixtureDetailsDataSectionFormationTabOuterBoxStyle,
    fixtureDetailsDataSectionFormationTabOuterPlayersBoxStyle,
    fixtureDetailsDataSectionFormationTabOuterPlayersGridStyle
} from "../../styles/features/fixtureDetails/fixtureDetailsDataSectionFormationTabStyle";
import {FixtureDetailsDataSectionPlayerIndicator} from "./FixtureDetailsDataSectionPlayerIndicator";

type Props = {
    fixture?: FixtureDto
};
export const FixtureDetailsDataSectionFormationTab = ({fixture}: Props) => {

    const [homeLineup, setHomeLineup] = useState<LineupDto | undefined>(undefined)
    const [awayLineup, setAwayLineup] = useState<LineupDto | undefined>(undefined)
    const [homePlayers, setHomePlayers] = useState<PlayerDto[]>([])
    const [awayPlayers, setAwayPlayers] = useState<PlayerDto[]>([])
    const [loading, setLoading] = useState(false)

    const imageRef = useRef<HTMLImageElement>(null)
    const gridRef = useRef<HTMLDivElement>(null)
    const [height, setHeight] = useState<number | undefined>(undefined)
    useResizeObserver(imageRef.current, entry => setHeight(entry.contentRect.height))

    const [homeLineupForGrid, setHomeLineupForGrid] = useState<{ column: number, players: PlayerDto[] }[]>([])
    const [awayLineupForGrid, setAwayLineupForGrid] = useState<{ column: number, players: PlayerDto[] }[]>([])
    useEffect(() => {
        if (homeLineup) {
            setHomeLineupForGrid([])
            let count = homeLineup.formation ? homeLineup.formation.split("-").length + 1 : null
            if (count) {
                for (let i = 1; i <= count; i++) {
                    setHomeLineupForGrid(oldArr => [...oldArr, {
                        column: i,
                        players: homePlayers.filter(p => p.gridX === i)
                    }])
                }
            }
        }
        if (awayLineup) {
            setAwayLineupForGrid([])
            let count = awayLineup.formation ? awayLineup.formation.split("-").length + 1 : null
            if (count) {
                for (let i = 1; i <= count; i++) {
                    setAwayLineupForGrid(oldArr => [...oldArr, {
                        column: i,
                        players: awayPlayers.filter(p => p.gridX === i)
                    }])
                }
            }
        }
    }, [homeLineup, awayLineup])

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
        <Box
            sx={fixtureDetailsDataSectionFormationTabOuterBoxStyle}
        >
            <Box
                sx={fixtureDetailsDataSectionFormationTabOuterPlayersBoxStyle}
            >
                <Box
                    sx={fixtureDetailsDataSectionFormationTabImageBoxStyle}
                    component="img"
                    src="/images/footballFieldBackground.jpg"
                    alt="Football field"
                    ref={imageRef}
                />

                <Grid container sx={fixtureDetailsDataSectionFormationTabOuterPlayersGridStyle} ref={gridRef}>
                    <Grid item xs={6}>
                        <Grid container sx={{height: height}}>
                            {homeLineupForGrid.map((element, i) => (
                                <Grid
                                    item
                                    xs={12 / homeLineupForGrid.length}
                                    sx={{display: "flex", flexDirection: "column", justifyContent: "space-evenly"}}
                                    key={i}
                                >
                                    {element.players.map((p, i) => (
                                        <FixtureDetailsDataSectionPlayerIndicator
                                            key={i}
                                            player={p}
                                            container={gridRef}
                                            color={p.pos === "G" ? "0ff" : "f0f"}
                                            borderColor="000"/>
                                    ))}
                                </Grid>
                            ))
                            }
                        </Grid>
                    </Grid>
                    <Grid item xs={6}>
                        <Grid container sx={{height: height}}>
                            {awayLineupForGrid.sort((a, b) => b.column - a.column).map((element, i) => (
                                <Grid
                                    item
                                    xs={12 / awayLineupForGrid.length}
                                    sx={{display: "flex", flexDirection: "column", justifyContent: "space-evenly"}}
                                    key={i}
                                >
                                    {element.players.map((p, i) => (
                                        <FixtureDetailsDataSectionPlayerIndicator
                                            key={i}
                                            player={p}
                                            container={gridRef}
                                            color={p.pos === "G" ? "f00" : "fff"}
                                            borderColor="000"/>
                                    ))}
                                </Grid>
                            ))
                            }
                        </Grid>
                    </Grid>
                </Grid>
            </Box>
        </Box>
    )
};