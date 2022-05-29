// @flow
import * as React from 'react';
import {Grid} from "@mui/material";
import {
    fixtureDetailsDataSectionStatisticsTabItemStyle
} from "../../styles/features/fixtureDetails/fixtureDetailsDataSectionStatisticsTabStyle";

type Props = {
    homeTeamText: string
    awayTeamText: string
    middleText: string
    first?: boolean
};
export const FixtureDetailsDataSectionStatisticsTabItem = ({homeTeamText, awayTeamText, middleText, first}: Props) => {

    return (
        <>
            <Grid item xs={2} sx={{...fixtureDetailsDataSectionStatisticsTabItemStyle, ...(first ? {border: "none"} : {})}}>
                {homeTeamText}
            </Grid>
            <Grid item xs={8} sx={{...fixtureDetailsDataSectionStatisticsTabItemStyle, ...(first ? {border: "none"} : {})}}>
                {middleText}
            </Grid>
            <Grid item xs={2} sx={{...fixtureDetailsDataSectionStatisticsTabItemStyle, ...(first ? {border: "none"} : {})}}>
                {awayTeamText}
            </Grid>
        </>
    );
};