// @flow
import * as React from 'react';
import {Box, Tab, Tabs, Typography} from "@mui/material";
import {
    fixtureDetailsDataSectionTabsLabelStyle,
    fixtureDetailsDataSectionTabsTabsStyle
} from "../../styles/features/fixtureDetails/fixtureDetailsDataSectionStyle";

type Props = {
    selectedTab: string
    setSelectedTab: (value: string) => void
};
export const FixtureDetailsDataSectionTabs = ({selectedTab, setSelectedTab}: Props) => {
    return (
        <Box>
            <Tabs
                value={selectedTab}
                onChange={(e, v) => setSelectedTab(v)}
                sx={fixtureDetailsDataSectionTabsTabsStyle}
                variant="scrollable"
                scrollButtons
                allowScrollButtonsMobile
            >
                <Tab value="stats" label={<Typography sx={fixtureDetailsDataSectionTabsLabelStyle}>Statistics</Typography>}/>
                <Tab value="lineups" label={<Typography sx={fixtureDetailsDataSectionTabsLabelStyle}>Lineups</Typography>}/>
                <Tab value="formation" label={<Typography sx={fixtureDetailsDataSectionTabsLabelStyle}>Formation</Typography>}/>
            </Tabs>
        </Box>
    );
};