// @flow 
import * as React from 'react';
import {Box, CircularProgress, LinearProgress, Typography} from "@mui/material";
import {useEffect, useState} from "react";
import {FixtureDto, FixtureListParams} from "../../app/models/football/fixtures";
import {PaginatedRequestData, PaginatedResponseData} from "../../app/models/common/pagination";
import agent from "../../app/api/agent";
import {dashboardListStyle} from "../../styles/features/dashboard/dashboardListStyle";
import {DashboardListItem} from "./DashboardListItem";
import SearchIcon from '@mui/icons-material/Search';
import InfiniteScroll from 'react-infinite-scroller'


const getGroupedFixtures = (fixtures: FixtureDto[], reverse: boolean) => {
    return fixtures.sort((a, b) => reverse ? b.date.unix() - a.date.unix() : a.date.unix() - b.date.unix()).reduce<{ [index: string]: FixtureDto[] }>((groups, fixture) => {
        const date = fixture.date.toISOString().split('T')[0]
        if (!groups[date]) {
            groups[date] = []
        }
        groups[date].push(fixture);
        return groups;

    }, {})
}

type Props = {
    fixtureListParams: FixtureListParams
};
export const DashboardList = ({fixtureListParams}: Props) => {

    const [fixtures, setFixtures] = useState<FixtureDto[]>([]);
    const [paginatedResponseData, setPaginatedResponseData] = useState<PaginatedResponseData | undefined>(undefined);
    const [loading, setLoading] = useState(false)
    const [groupedFixtures, setGroupedFixtures] = useState<{ [index: string]: FixtureDto[] }>({})

    useEffect(() => {
        setLoading(true)
        setFixtures([])
        agent.Fixtures.list({
            currentPage: 1,
            pageSize: 10
        }, fixtureListParams)
            .then(res => {
                setPaginatedResponseData({
                    pageSize: res.data.pageSize,
                    currentPage: res.data.currentPage,
                    totalResults: res.data.totalResults,
                    totalPages: res.data.totalPages
                })
                setFixtures(res.data.items)
                setLoading(false)
            })
    }, [fixtureListParams])

    useEffect(() => {
        setGroupedFixtures(getGroupedFixtures(fixtures, fixtureListParams.type == "ended" || fixtureListParams.type == "cancelled"))
    }, [fixtures])

    if (loading)
        return (
            <Box sx={{
                ...dashboardListStyle,
                py: "auto",
                display: "flex",
                flexDirection: "row",
                justifyContent: "center",
                alignItems: "center"
            }}>
                <CircularProgress color="secondary" size={100}/>
            </Box>
        )

    if (fixtures.length === 0)
        return (
            <Box sx={{
                ...dashboardListStyle,
                py: "auto",
                display: "flex",
                flexDirection: "row",
                justifyContent: "center"
            }}>
                <SearchIcon sx={{fontSize: "4rem", mr: "2rem", my: "auto"}}/>
                <Typography
                    sx={{fontSize: "4rem", my: "auto"}}
                >
                    No results
                </Typography>
            </Box>
        )


    return (
        <Box sx={dashboardListStyle}>
            <Typography
            >
                Showing {fixtures.length} out of {paginatedResponseData?.totalResults} total.
            </Typography>
            <InfiniteScroll
                pageStart={1}
                initialLoad={false}
                loadMore={(pageNumber) => {
                    agent.Fixtures.list({
                        currentPage: pageNumber,
                        pageSize: 10
                    }, fixtureListParams)
                        .then(res => {
                            setFixtures(prev => [...prev, ...res.data.items])
                            setPaginatedResponseData({
                                pageSize: res.data.pageSize,
                                currentPage: res.data.currentPage,
                                totalResults: res.data.totalResults,
                                totalPages: res.data.totalPages
                            })
                        })
                }}
                hasMore={(paginatedResponseData?.currentPage || 0) < (paginatedResponseData?.totalPages || 0)}
                loader={
                    <Box sx={{my: {xs: "0.5rem", md: "3rem"}, width: "100%"}} key={1337}>
                        <LinearProgress color="primary" sx={{mx: "auto", width: "10rem", height: "1rem"}}/>
                    </Box>
                }
            >
                {Object.keys(groupedFixtures).map((date, i) => (
                    <Box key={i}>
                        <Typography
                            variant="h4"
                            sx={{borderBottom: "1px solid", borderColor: "primary.main", mt: "2rem"}}
                        >
                            {date}
                        </Typography>
                        {groupedFixtures[date].map((fixture) => (
                            <DashboardListItem fixture={fixture} key={fixture.id}/>
                        ))}
                    </Box>
                ))}
            </InfiniteScroll>
        </Box>
    );
};