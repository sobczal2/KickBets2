// @flow
import * as React from 'react';
import {useParams} from "react-router-dom";
import {useEffect, useState} from "react";
import {FixtureDto} from "../models/football/fixtures";
import agent from "../api/agent";

type Props = {

};
export const FixtureDetailsPage = (props: Props) => {
    const params = useParams<"fixtureId">();

    const [fixture, setFixture] = useState<FixtureDto>()
    const [loading, setLoading] = useState(false)

    useEffect(() => {
        setLoading(true)
        if(!params.fixtureId) return
        agent.Fixtures.getById(parseInt(params.fixtureId))
            .then(res => {
                setFixture(res.data)
            })
            .finally(() => setLoading(false))
    }, [params, params.fixtureId])

    if(loading)
        return (
            <div>
                loading
            </div>
        )

    return (
        <div>
            Fixture with id: {params.fixtureId}
        </div>
    );
};