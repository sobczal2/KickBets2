// @flow 
import * as React from 'react';
import {useStore} from "../stores/store";
import {Box, Button, Grid} from "@mui/material";
import {Link} from "react-router-dom";
import {useEffect, useState} from "react";
import {BaseBetDto} from "../models/bets/bets";
import agent from "../api/agent";
import {MyBetsListItem} from "../../features/myBets/MyBetsListItem";

type Props = {
    
};
export const MyBetsPage = (props: Props) => {
    
    const store = useStore()

    const [bets, setBets] = useState<BaseBetDto[]>([])

    useEffect(() => {

        agent.Bets.getMyBets({currentPage: 1, pageSize: 100})
            .then(res => {
                setBets(res.data.items)
            })

    }, [])
    
    if(!store.identityStore.user)
    {
        return (
            <Box sx={{width: "100%", py: "10rem", textAlign: "center"}}>
                <Button
                    variant="outlined"
                    component={Link}
                    to="/login"
                    sx={{fontSize: "3rem"}}
                >
                    Go to login
                </Button>
            </Box>
        )
    }

    if(bets.length === 0)
    {
        return (
            <Box sx={{width: "100%", py: "10rem", textAlign: "center"}}>
                <Button
                    variant="outlined"
                    component={Link}
                    to="/fixtures"
                    sx={{fontSize: "3rem"}}
                >
                    No bets yet, go make some!
                </Button>
            </Box>
        )
    }
    
    return (
        <Grid container sx={{width: "100%", py: "2rem", display: "flex"}}>
            {bets.map((bet, i) => (
                <MyBetsListItem bet={bet} key={i} />
            ))}
        </Grid>
    );
};