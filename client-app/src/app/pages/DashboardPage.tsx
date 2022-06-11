// @flow
import * as React from 'react';
import {useState} from 'react';
import {FixtureListParams} from "../models/football/fixtures";
import {DashboardList} from "../../features/dashboard/DashboardList";
import {Box, Grid} from "@mui/material";
import {DashboardPageStyle} from "../../styles/app/pages/dashboardPageStyle";
import {DashboardFilter} from "../../features/dashboard/DashboardFilter";

type Props = {};
export const DashboardPage = (props: Props) => {

    const [fixtureListParams, setFixtureListParams] = useState<FixtureListParams>({type: "upcoming", leagueId: null});

    return (
        <Box sx={DashboardPageStyle}>
            <Grid container>
                <Grid item xs={10}>
                    <DashboardList fixtureListParams={fixtureListParams}/>
                </Grid>
                <Grid item xs={2}>
                    <DashboardFilter fixtureListParams={fixtureListParams} setFixtureListParams={setFixtureListParams}/>
                </Grid>
            </Grid>
        </Box>
    );
};