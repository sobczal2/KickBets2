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
                <Tab
                    id="details-statistics-tab-selector"
                    value="stats"
                     label={<Typography sx={fixtureDetailsDataSectionTabsLabelStyle}>Statistics</Typography>}/>
                <Tab
                    id="details-lineups-tab-selector"
                    value="lineups"
                     label={<Typography sx={fixtureDetailsDataSectionTabsLabelStyle}>Lineups</Typography>}/>
                <Tab
                    id="details-formation-tab-selector"
                    value="formation"
                     label={<Typography sx={fixtureDetailsDataSectionTabsLabelStyle}>Formation</Typography>}/>
            </Tabs>
        </Box>
    );
};