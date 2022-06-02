// @flow 
import * as React from 'react';
import {Box} from "@mui/material";
import {FixtureDto} from "../../app/models/football/fixtures";
import {FixtureDetailsDataSectionInfo} from "./FixtureDetailsDataSectionInfo";
import {FixtureDetailsDataSectionScore} from "./FixtureDetailsDataSectionScore";
import {FixtureDetailsDataSectionTabs} from "./FixtureDetailsDataSectionTabs";
import {useState} from "react";
import {FixtureDetailsDataSectionStatisticsTab} from "./FixtureDetailsDataSectionStatisticsTab";
import {FixtureDetailsDataSectionLineupsTab} from "./FixtureDetailsDataSectionLineupsTab";
import {FixtureDetailsDataSectionFormationTab} from "./FixtureDetailsDataSectionFormationTab";

type Props = {
    fixture?: FixtureDto
};
export const FixtureDetailsDataSection = ({fixture}: Props) => {

    const [selectedTab, setSelectedTab] = useState("stats")

    return (
        <Box>
            <FixtureDetailsDataSectionInfo fixture={fixture}/>
            <FixtureDetailsDataSectionScore fixture={fixture}/>
            <FixtureDetailsDataSectionTabs selectedTab={selectedTab} setSelectedTab={setSelectedTab}/>
            {selectedTab === "stats" && <FixtureDetailsDataSectionStatisticsTab fixture={fixture}/>}
            {selectedTab === "lineups" && <FixtureDetailsDataSectionLineupsTab fixture={fixture} />}
            {selectedTab === "formation" && <FixtureDetailsDataSectionFormationTab fixture={fixture}/>}
        </Box>
    );
};