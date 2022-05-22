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
import InfiniteScroll from "react-infinite-scroll-component";

type Props = {
    fixtureListParams: FixtureListParams
};
export const DashboardList = ({fixtureListParams}: Props) => {

    const [fixtures, setFixtures] = useState<FixtureDto[]>([]);
    const [paginatedRequestData, setPaginatedRequestData] = useState<PaginatedRequestData>({
        currentPage: 1,
        pageSize: 20
    });
    const [paginatedResponseData, setPaginatedResponseData] = useState<PaginatedResponseData | undefined>(undefined);
    const [loading, setLoading] = useState(false)

    useEffect(() => {
        setLoading(true)
        setFixtures([])
        agent.Fixtures.list(paginatedRequestData, fixtureListParams)
            .then(res => {
                console.log(res)
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

            <InfiniteScroll
                dataLength={fixtures.length}
                next={() => {
                    console.log(paginatedRequestData)
                    // @ts-ignore
                    setPaginatedRequestData({currentPage: paginatedRequestData.currentPage + 1, pageSize: paginatedRequestData.pageSize})
                    agent.Fixtures.list(paginatedRequestData, fixtureListParams)
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
                hasMore={paginatedRequestData.currentPage! <= (paginatedResponseData?.totalPages || 1)}
                loader={
                    <Box sx={{my: {xs: "0.5rem", md: "3rem"}, width: "100%"}}>
                        <LinearProgress color="primary" sx={{mx: "auto", width: "10rem", height: "1rem"}}/>
                    </Box>
                }
            >
                {fixtures.map((f, i) => (
                    <DashboardListItem key={i} fixture={f}/>
                ))}
            </InfiniteScroll>
        </Box>
    );
};