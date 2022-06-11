// @flow
import * as React from 'react';
import {useEffect, useState} from 'react';
import {FixtureDto} from "../../app/models/football/fixtures";
import {Box} from "@mui/material";
import {AccessTimeFilled, Flag, Place, Sports} from "@mui/icons-material";
import {LeagueDto} from "../../app/models/football/leagues";
import {VenueDto} from "../../app/models/football/venues";
import agent from "../../app/api/agent";
import {
    fixtureDetailsDataSectionInfoInnerBoxStyle,
    fixtureDetailsDataSectionInfoOuterBoxStyle
} from "../../styles/features/fixtureDetails/fixtureDetailsDataSectionStyle";

type Props = {
    fixture?: FixtureDto
};
export const FixtureDetailsDataSectionInfo = ({fixture}: Props) => {

    const [league, setLeague] = useState<LeagueDto | undefined>(undefined)
    const [venue, setVenue] = useState<VenueDto | undefined>(undefined)

    useEffect(() => {
        if (fixture && fixture.venueId) {
            agent.Venues.getById(fixture.venueId)
                .then(res => {
                    setVenue(res.data)
                })
        }
    }, [fixture, fixture?.venueId])

    useEffect(() => {
        if (fixture && fixture.venueId) {
            agent.Leagues.getById(fixture.leagueId)
                .then(res => {
                    setLeague(res.data)
                })
        }
    }, [fixture, fixture?.leagueId])

    const backgroundStyle = {
        backgroundImage: venue?.image ? `linear-gradient(rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0.5)), url(${venue.image})` : "linear-gradient(rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0.5)), url(/images/homeBackgroundImage.jpg)",
        backgroundColor: "primary.main",
        backgroundSize: "cover",
        backgroundRepeat: "no-repeat",
        backgroundPosition: "50% 50%",
    }

    return (
        <Box sx={{...fixtureDetailsDataSectionInfoOuterBoxStyle, ...backgroundStyle}}>
            <Box sx={fixtureDetailsDataSectionInfoInnerBoxStyle}>
                <Flag sx={{my: "auto", mx: "0.5rem", fontSize: "3rem"}}/>
                {league?.name || "unknown"}, {league?.country || "unknown"}
            </Box>
            <Box sx={fixtureDetailsDataSectionInfoInnerBoxStyle}>
                <AccessTimeFilled sx={{my: "auto", mx: "0.5rem", fontSize: "3rem"}}/>
                {(fixture && fixture.date.toString()) || "unknown"}
            </Box>
            <Box sx={fixtureDetailsDataSectionInfoInnerBoxStyle}>
                <Place sx={{my: "auto", mx: "0.5rem", fontSize: "3rem"}}/>
                {venue?.name || "unknown"}, {venue?.city || "unknown"}
            </Box>
            <Box sx={fixtureDetailsDataSectionInfoInnerBoxStyle}>
                <Sports sx={{my: "auto", mx: "0.5rem", fontSize: "3rem"}}/>
                {(fixture && fixture.referee) || "unknown"}
            </Box>
        </Box>
    );
};