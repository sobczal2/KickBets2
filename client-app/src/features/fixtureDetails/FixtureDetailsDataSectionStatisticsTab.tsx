// @flow
import * as React from 'react';
import {useEffect, useState} from 'react';
import {Box, CircularProgress, Grid, Typography} from "@mui/material";
import {FixtureDto} from "../../app/models/football/fixtures";
import {
    fixtureDetailsDataSectionStatisticsTabOuterBoxStyle
} from "../../styles/features/fixtureDetails/fixtureDetailsDataSectionStatisticsTabStyle";
import {StatisticDto} from "../../app/models/football/statistics";
import agent from "../../app/api/agent";
import {FixtureDetailsDataSectionStatisticsTabItem} from "./FixtureDetailsDataSectionStatisticsTabItem";

type Props = {
    fixture?: FixtureDto
};
export const FixtureDetailsDataSectionStatisticsTab = ({fixture}: Props) => {

    const [homeStatistic, setHomeStatistic] = useState<StatisticDto | undefined>(undefined)
    const [awayStatistic, setAwayStatistic] = useState<StatisticDto | undefined>(undefined)
    const [loading, setLoading] = useState(false)

    useEffect(() => {
        if (!(fixture && fixture.homeStatisticsId && fixture.awayStatisticsId)) return
        let fetched = 0
        setLoading(true)
        agent.Statistics.getById(fixture.homeStatisticsId)
            .then(res => {
                setHomeStatistic(res.data)
            })
            .finally(() => {
                if (fetched === 1)
                    setLoading(false)
                fetched += 1
            })
        agent.Statistics.getById(fixture.awayStatisticsId)
            .then(res => {
                setAwayStatistic(res.data)
            })
            .finally(() => {
                if (fetched === 1)
                    setLoading(false)
                fetched += 1
            })
    }, [fixture, fixture?.homeStatisticsId, fixture?.awayStatisticsId])

    if (loading) {
        return (
            <Box
                id="details-statistics-tab"
                sx={{
                ...fixtureDetailsDataSectionStatisticsTabOuterBoxStyle,
                display: "flex",
                alignItems: "center",
                justifyContent: "center"
            }}>
                <CircularProgress color="secondary"/>
            </Box>
        )
    }

    if (!homeStatistic || !awayStatistic) {
        return (
            <Box
                id="details-statistics-tab"
                sx={{
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
            id="details-statistics-tab"
            sx={fixtureDetailsDataSectionStatisticsTabOuterBoxStyle}>
            <Grid container sx={{width: "100%"}}>
                <FixtureDetailsDataSectionStatisticsTabItem
                    homeTeamText={homeStatistic.shotsOnGoal ? homeStatistic.shotsOnGoal.toString() : "0"}
                    awayTeamText={awayStatistic.shotsOnGoal ? awayStatistic.shotsOnGoal.toString() : "0"}
                    middleText="shots on goal"
                    first
                />
                <FixtureDetailsDataSectionStatisticsTabItem
                    homeTeamText={homeStatistic.shotsOffGoal ? homeStatistic.shotsOffGoal.toString() : "0"}
                    awayTeamText={awayStatistic.shotsOffGoal ? awayStatistic.shotsOffGoal.toString() : "0"}
                    middleText="shots off goal"/>
                <FixtureDetailsDataSectionStatisticsTabItem
                    homeTeamText={homeStatistic.shotsInsideBox ? homeStatistic.shotsInsideBox.toString() : "0"}
                    awayTeamText={awayStatistic.shotsInsideBox ? awayStatistic.shotsInsideBox.toString() : "0"}
                    middleText="shots inside box"/>
                <FixtureDetailsDataSectionStatisticsTabItem
                    homeTeamText={homeStatistic.shotsOutsideBox ? homeStatistic.shotsOutsideBox.toString() : "0"}
                    awayTeamText={awayStatistic.shotsOutsideBox ? awayStatistic.shotsOutsideBox.toString() : "0"}
                    middleText="shots outside box"/>
                <FixtureDetailsDataSectionStatisticsTabItem
                    homeTeamText={homeStatistic.totalShots ? homeStatistic.totalShots.toString() : "0"}
                    awayTeamText={awayStatistic.totalShots ? awayStatistic.totalShots.toString() : "0"}
                    middleText="total shots"/>
                <FixtureDetailsDataSectionStatisticsTabItem
                    homeTeamText={homeStatistic.blockedShots ? homeStatistic.blockedShots.toString() : "0"}
                    awayTeamText={awayStatistic.blockedShots ? awayStatistic.blockedShots.toString() : "0"}
                    middleText="blocked shots"/>
                <FixtureDetailsDataSectionStatisticsTabItem
                    homeTeamText={homeStatistic.fouls ? homeStatistic.fouls.toString() : "0"}
                    awayTeamText={awayStatistic.fouls ? awayStatistic.fouls.toString() : "0"}
                    middleText="fouls"/>
                <FixtureDetailsDataSectionStatisticsTabItem
                    homeTeamText={homeStatistic.cornerKicks ? homeStatistic.cornerKicks.toString() : "0"}
                    awayTeamText={awayStatistic.cornerKicks ? awayStatistic.cornerKicks.toString() : "0"}
                    middleText="corner kicks"/>
                <FixtureDetailsDataSectionStatisticsTabItem
                    homeTeamText={homeStatistic.offsides ? homeStatistic.offsides.toString() : "0"}
                    awayTeamText={awayStatistic.offsides ? awayStatistic.offsides.toString() : "0"}
                    middleText="offsides"/>
                <FixtureDetailsDataSectionStatisticsTabItem
                    homeTeamText={homeStatistic.possession ? `${(homeStatistic.possession * 100).toFixed(0).toString()}%` : "0%"}
                    awayTeamText={awayStatistic.possession ? `${(awayStatistic.possession * 100).toFixed(0).toString()}%` : "0%"}
                    middleText="possession"/>
                <FixtureDetailsDataSectionStatisticsTabItem
                    homeTeamText={homeStatistic.yellowCards ? homeStatistic.yellowCards.toString() : "0"}
                    awayTeamText={awayStatistic.yellowCards ? awayStatistic.yellowCards.toString() : "0"}
                    middleText="yellow cards"/>
                <FixtureDetailsDataSectionStatisticsTabItem
                    homeTeamText={homeStatistic.redCards ? homeStatistic.redCards.toString() : "0"}
                    awayTeamText={awayStatistic.redCards ? awayStatistic.redCards.toString() : "0"}
                    middleText="red cards"/>
                <FixtureDetailsDataSectionStatisticsTabItem
                    homeTeamText={homeStatistic.goalkeeperSaves ? homeStatistic.goalkeeperSaves.toString() : "0"}
                    awayTeamText={awayStatistic.goalkeeperSaves ? awayStatistic.goalkeeperSaves.toString() : "0"}
                    middleText="goalkeeper saves"/>
                <FixtureDetailsDataSectionStatisticsTabItem
                    homeTeamText={homeStatistic.totalPasses ? homeStatistic.totalPasses.toString() : "0"}
                    awayTeamText={awayStatistic.totalPasses ? awayStatistic.totalPasses.toString() : "0"}
                    middleText="total passes"/>
                <FixtureDetailsDataSectionStatisticsTabItem
                    homeTeamText={homeStatistic.accuratePasses ? homeStatistic.accuratePasses.toString() : "0"}
                    awayTeamText={awayStatistic.accuratePasses ? awayStatistic.accuratePasses.toString() : "0"}
                    middleText="accurate passes"/>
                <FixtureDetailsDataSectionStatisticsTabItem
                    homeTeamText={homeStatistic.passes ? `${(homeStatistic.passes * 100).toFixed(0).toString()}%` : "0%"}
                    awayTeamText={awayStatistic.passes ? `${(awayStatistic.passes * 100).toFixed(0).toString()}%` : "0%"}
                    middleText="passes"/>
            </Grid>
        </Box>
    );
};