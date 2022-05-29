// @flow 
import * as React from 'react';
import {FixtureDto} from "../../app/models/football/fixtures";
import {Box} from "@mui/material";
import {
    fixtureDetailsBetsSectionOuterBoxStyle
} from "../../styles/features/fixtureDetails/fixtureDetailsBetsSectionStyle";
import {FixtureDetailsBetsSectionWdlhtDisplay} from "./FixtureDetailsBetsSectionWdlhtDisplay";
import {FixtureDetailsBetsSectionWdlftDisplay} from "./FixtureDetailsBetsSectionWdlftDisplay";

type Props = {
    fixture?: FixtureDto
};
export const FixtureDetailsBetsSection = ({fixture}: Props) => {
    return (
        <Box sx={fixtureDetailsBetsSectionOuterBoxStyle}>
            <FixtureDetailsBetsSectionWdlhtDisplay fixture={fixture}/>
            <FixtureDetailsBetsSectionWdlftDisplay fixture={fixture}/>
        </Box>
    );
};