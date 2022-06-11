// @flow
import * as React from 'react';
import {useEffect, useState} from 'react';
import {FixtureListParams} from "../../app/models/football/fixtures";
import {Box, FormControl, FormControlLabel, FormLabel, Radio, RadioGroup} from "@mui/material";
import {
    dashboardFilterStyle,
    dashboardFilterTypeMainLabelStyle,
    dashboardFilterTypeMinorLabelStyle,
    dashboardFilterTypeRadioStyle
} from "../../styles/features/dashboard/dashboardFilterStyle";
import {LeagueDto} from "../../app/models/football/leagues";
import agent from "../../app/api/agent";

type Props = {
    fixtureListParams: FixtureListParams
    setFixtureListParams: (fixtureListParams: FixtureListParams) => void
};
export const DashboardFilter = ({fixtureListParams, setFixtureListParams}: Props) => {

    const [leagues, setLeagues] = useState<LeagueDto[]>([])

    useEffect(() => {
        agent.Leagues.list({currentPage: 1, pageSize: 50})
            .then(res => {
                setLeagues(res.data.items)
            })
    }, [])

    return (
        <Box sx={dashboardFilterStyle}>
            <FormControl sx={{m: "2.5rem"}}>
                <FormLabel
                    id="dashboard-filter-type-radio-label"
                    sx={dashboardFilterTypeMainLabelStyle}
                >
                    Type
                </FormLabel>
                <RadioGroup
                    aria-labelledby="dashboard-filter-type-radio-label"
                    value={fixtureListParams.type}
                    onChange={e => setFixtureListParams({
                        ...fixtureListParams,
                        type: e.target.value as "all" | "upcoming" | "ended" | "cancelled"
                    })}
                    name="dashboard-filter-type-radio"
                >
                    <FormControlLabel value="all" control={<Radio sx={dashboardFilterTypeRadioStyle}/>}
                                      label="All matches" sx={dashboardFilterTypeMinorLabelStyle}/>
                    <FormControlLabel value="upcoming" control={<Radio sx={dashboardFilterTypeRadioStyle}/>}
                                      label="Upcoming matches" sx={dashboardFilterTypeMinorLabelStyle}/>
                    <FormControlLabel value="ended" control={<Radio sx={dashboardFilterTypeRadioStyle}/>}
                                      label="Ended matches" sx={dashboardFilterTypeMinorLabelStyle}/>
                    <FormControlLabel value="cancelled" control={<Radio sx={dashboardFilterTypeRadioStyle}/>}
                                      label="Cancelled matches" sx={dashboardFilterTypeMinorLabelStyle}/>
                </RadioGroup>
            </FormControl>
            <FormControl sx={{m: "2.5rem"}}>
                <FormLabel
                    id="dashboard-filter-type-radio-label"
                    sx={dashboardFilterTypeMainLabelStyle}
                >
                    Leagues
                </FormLabel>
                <RadioGroup
                    aria-labelledby="dashboard-filter-type-radio-label"
                    value={fixtureListParams.leagueId ? fixtureListParams.leagueId.toString() : "0"}
                    onChange={e => setFixtureListParams({
                        ...fixtureListParams,
                        leagueId: e.target.value === "0" ? null : parseInt(e.target.value)
                    })}
                    name="dashboard-filter-type-radio"
                >
                    <FormControlLabel value={"0"} control={<Radio sx={dashboardFilterTypeRadioStyle}/>}
                                      label="All matches" sx={dashboardFilterTypeMinorLabelStyle}/>
                    {leagues.map((l, i) => (
                        <FormControlLabel key={i} value={l.id.toString()}
                                          control={<Radio sx={dashboardFilterTypeRadioStyle}/>} label={l.name}
                                          sx={dashboardFilterTypeMinorLabelStyle}/>
                    ))}
                </RadioGroup>
            </FormControl>
        </Box>
    );
};