// @flow
import * as React from 'react';
import {useEffect, useState} from 'react';
import {useParams} from "react-router-dom";
import {FixtureDto} from "../models/football/fixtures";
import agent from "../api/agent";
import {Backdrop, CircularProgress, Grid} from "@mui/material";
import {FixtureDetailsDataSection} from "../../features/fixtureDetails/FixtureDetailsDataSection";
import {FixtureDetailsBetsSection} from "../../features/fixtureDetails/FixtureDetailsBetsSection";

type Props = {};
export const FixtureDetailsPage = (props: Props) => {
    const params = useParams<"fixtureId">();

    const [fixture, setFixture] = useState<FixtureDto>()
    const [loading, setLoading] = useState(false)

    useEffect(() => {
        setLoading(true)
        if (!params.fixtureId) return
        agent.Fixtures.getById(parseInt(params.fixtureId))
            .then(res => {
                setFixture(res.data)
            })
            .finally(() => setLoading(false))
    }, [params, params.fixtureId])

    useEffect(() => {
        window.scrollTo(0, 0)
    }, [])

    return (
        <>
            <Backdrop
                open={loading}
            >
                <CircularProgress color="secondary"/>
            </Backdrop>
            <Grid container>
                <Grid item xs={8}>
                    <FixtureDetailsDataSection fixture={fixture}/>
                </Grid>
                <Grid item xs={4}>
                    <FixtureDetailsBetsSection fixture={fixture}/>
                </Grid>
            </Grid>
        </>

    );
};