// @flow
import * as React from 'react';
import {useEffect, useState} from 'react';
import {FixtureDto} from "../../app/models/football/fixtures";
import {Box, Grid, Typography} from "@mui/material";
import {useNavigate} from "react-router-dom";
import {LeagueDto} from "../../app/models/football/leagues";
import {
    dashboardListItemGridItemStyle,
    dashboardListItemLeagueFlagStyle,
    dashboardListItemLongStatusStyle,
    dashboardListItemMiddleSectionStyle,
    dashboardListItemResultStyle,
    dashboardListItemStyle,
    dashboardListItemTeamLogoStyle,
    dashboardListItemTeamNameStyle,
    dashboardListItemTimeStyle
} from "../../styles/features/dashboard/dashboardListItemStyle";
import agent from "../../app/api/agent";
import {StatusDto} from "../../app/models/football/statuses";
import dayjs from "dayjs";
import LocalizedFormat from 'dayjs/plugin/localizedFormat';
import {TeamDto} from "../../app/models/football/teams";
import {ScoreDto} from "../../app/models/football/scores";
import {useStore} from "../../app/stores/store";
import {observer} from "mobx-react-lite";

type Props = {
    fixture: FixtureDto
};
export const DashboardListItem = observer(({fixture}: Props) => {

    const store = useStore()
    const navigate = useNavigate()
    dayjs.extend(LocalizedFormat)

    const [league, setLeague] = useState<LeagueDto | undefined>(undefined)
    const [status, setStatus] = useState<StatusDto | undefined>(undefined)
    const [homeTeam, setHomeTeam] = useState<TeamDto | undefined>(undefined)
    const [awayTeam, setAwayTeam] = useState<TeamDto | undefined>(undefined)
    const [score, setScore] = useState<ScoreDto | undefined>(undefined)

    const started = ["FT", "AET", "PEN", "1H", "HT", "2H", "ET", "P", "BT", "LIVE"].includes(status?.short || "")

    useEffect(() => {
        store.leagueStore.getLeagueById(fixture.leagueId)
            .then(league => {
                setLeague(league)
            })
        agent.Statuses.getById(fixture.statusId)
            .then(res => {
                setStatus(res.data)
            })
        if (fixture.homeTeamId) {
            store.teamStore.getTeamById(fixture.homeTeamId)
                .then(team => {
                    setHomeTeam(team)
                })
        }
        if (fixture.awayTeamId) {
            store.teamStore.getTeamById(fixture.awayTeamId)
                .then(team => {
                    setAwayTeam(team)
                })
        }
        agent.Scores.getById(fixture.scoreId)
            .then(res => {
                setScore(res.data)
            })

    }, [fixture, store.leagueStore, store.teamStore])

    return (
        <Box sx={dashboardListItemStyle} className="dashboard-list-item-box">
            <Grid
                container
                onClick={() => navigate(`/fixtures/${fixture.id}`)}
                direction="row"
                sx={{height: "100%"}}
            >
                <Grid
                    item
                    xs={1}
                    sx={dashboardListItemGridItemStyle}
                >
                    <Box
                        component="img"
                        alt="league flag"
                        src={league && league.logo ? league.logo : "/images/placeholders/FlagUnavailablePlaceholder.png"}
                        sx={dashboardListItemLeagueFlagStyle}
                    />
                </Grid>
                <Grid item xs={2} sx={{display: 'flex', flexDirection: 'row', justifyContent: "right"}}>
                    <Box sx={{display: 'flex', flexDirection: 'column', textAlign: 'right', my: 'auto'}}>
                        <Typography
                            variant="h4"
                            sx={dashboardListItemTimeStyle}
                        >
                            {fixture.date.format("LT")}
                        </Typography>
                        <Typography
                            variant="h4"
                            sx={dashboardListItemLongStatusStyle}
                        >
                            {status?.long}
                        </Typography>
                    </Box>
                </Grid>
                <Grid item xs={2} sx={{textAlign: 'right', my: 'auto'}}>
                    <Typography sx={dashboardListItemTeamNameStyle}>
                        {homeTeam?.name || "TBD"}
                    </Typography>
                </Grid>
                <Grid item xs={5} sx={dashboardListItemMiddleSectionStyle}>
                    <Box
                        component="img"
                        alt="home team logo"
                        src={homeTeam?.logo || "/images/placeholders/TeamLogoUnavailablePlaceholder.png"}
                        sx={dashboardListItemTeamLogoStyle}
                    />
                    <Typography sx={dashboardListItemResultStyle}>
                        {started ? `${score?.homeCurrentScore} - ${score?.awayCurrentScore}` : "VS"}
                    </Typography>
                    <Box
                        component="img"
                        alt="home team logo"
                        src={awayTeam?.logo || "/images/placeholders/TeamLogoUnavailablePlaceholder.png"}
                        sx={dashboardListItemTeamLogoStyle}
                    />
                </Grid>
                <Grid item xs={2} sx={{textAlign: 'left', my: 'auto'}}>
                    <Typography sx={dashboardListItemTeamNameStyle}>
                        {awayTeam?.name || "TBD"}
                    </Typography>
                </Grid>
            </Grid>
        </Box>
    );
});