// @flow
import * as React from 'react';
import {
    Box, Button,
    Dialog,
    DialogContent,
    DialogTitle,
    FormControl, FormControlLabel,
    FormLabel,
    Grid, Radio,
    RadioGroup, TextField,
    Typography
} from "@mui/material";
import {
    fixtureDetailsBetsSectionDisplayHeaderStyle,
    fixtureDetailsBetsSectionDisplayInnerBoxStyle,
    fixtureDetailsBetsSectionDisplayMultiplierStyle,
    fixtureDetailsBetsSectionDisplayOuterBoxStyle,
    fixtureDetailsBetsSectionDisplayTitleStyle
} from "../../styles/features/fixtureDetails/fixtureDetailsBetsSectionStyle";
import {FixtureDto} from "../../app/models/football/fixtures";
import {useEffect, useState} from "react";
import {WdlhtBetsDataDto} from "../../app/models/bets/wdlhtBets";
import agent from "../../app/api/agent";
import dayjs from "dayjs";
import {useStore} from "../../app/stores/store";

type Props = {
    fixture?: FixtureDto
};
export const FixtureDetailsBetsSectionWdlftDisplay = ({fixture}: Props) => {

    const [wdlftData, setWdlftData] = useState<WdlhtBetsDataDto | undefined>(undefined)
    const [homeTeamName, setHomeTeamName] = useState<string | undefined>(undefined)
    const [awayTeamName, setAwayTeamName] = useState<string | undefined>(undefined)

    const [modalVisible, setModalVisible] = useState(false)

    const [side, setSide] = useState<"home" | "draw" | "away">("home")
    const [value, setValue] = useState<string>("")

    const store = useStore()

    const available = dayjs().isBefore(fixture?.date)

    useEffect(() => {
        if (fixture && fixture.betsDataId) {
            agent.Bets.getBetsData(fixture.betsDataId)
                .then(res => {
                    setWdlftData(res.data.wdlftBetsData)
                })
        }
    }, [fixture, fixture?.betsDataId])

    useEffect(() => {
        if (fixture && fixture.homeTeamId) {
            agent.Teams.getById(fixture.homeTeamId)
                .then(res => {
                    setHomeTeamName(res.data.name)
                })
        }
    }, [fixture, fixture?.homeTeamId])

    useEffect(() => {
        if (fixture && fixture.awayTeamId) {
            agent.Teams.getById(fixture.awayTeamId)
                .then(res => {
                    setAwayTeamName(res.data.name)
                })
        }
    }, [fixture, fixture?.awayTeamId])

    // @ts-ignore
    return (
        <Box
            sx={{...fixtureDetailsBetsSectionDisplayOuterBoxStyle, opacity: available ? "100%" : "50%"}}
            onClick={() => {
                if (available) setModalVisible(true)
            }}
        >
            <Dialog
                open={modalVisible}
                onClose={() => setModalVisible(false)}
            >
                <DialogTitle>Bet: Score at full time</DialogTitle>
                <DialogContent
                >
                    <FormControl>
                        <FormLabel>Side</FormLabel>
                        <RadioGroup
                            value={side}
                            onChange={e => setSide(e.target.value as "home" | "draw" | "away")}
                            sx={{display: "flex", flexDirection: "row"}}
                        >
                            <FormControlLabel value="home" control={<Radio/>} label={`${homeTeamName} win`}/>
                            <FormControlLabel value="draw" control={<Radio/>} label="draw"/>
                            <FormControlLabel value="away" control={<Radio/>} label={`${awayTeamName} win`}/>
                        </RadioGroup>
                    </FormControl>
                    <TextField
                        value={value}
                        onChange={e => {
                            setValue(e.target.value)
                        }}
                        // error
                        // label="Error"
                        // helperText="Incorrect entry."
                        variant="standard"
                    />
                    <Button
                        onClick={() => {
                            agent.Bets.createWdlftBet(fixture!.id, parseFloat(value), side)
                                .then(res => {
                                    setModalVisible(false)
                                    store.identityStore.aboutMe(false)
                                })
                        }}
                    >
                        Submit
                    </Button>
                </DialogContent>
            </Dialog>
            <Box sx={fixtureDetailsBetsSectionDisplayHeaderStyle}>
                Bet: Score at full time
            </Box>
            <Grid container>
                <Grid item xs={4}>
                    <Box sx={fixtureDetailsBetsSectionDisplayInnerBoxStyle}>
                        <Typography
                            sx={fixtureDetailsBetsSectionDisplayTitleStyle}
                        >
                            {homeTeamName} to win at full time
                        </Typography>
                        <Typography
                            sx={fixtureDetailsBetsSectionDisplayMultiplierStyle}
                        >
                            {wdlftData?.homeBetsMultiplier ? `x${wdlftData?.homeBetsMultiplier.toFixed(2)}` : "unknown"}
                        </Typography>
                    </Box>
                </Grid>
                <Grid item xs={4}>
                    <Box sx={fixtureDetailsBetsSectionDisplayInnerBoxStyle}>
                        <Typography
                            sx={fixtureDetailsBetsSectionDisplayTitleStyle}
                        >
                            Draw at full time
                        </Typography>
                        <Typography
                            sx={fixtureDetailsBetsSectionDisplayMultiplierStyle}
                        >
                            {wdlftData?.drawBetsMultiplier ? `x${wdlftData?.drawBetsMultiplier.toFixed(2)}` : "unknown"}
                        </Typography>
                    </Box>
                </Grid>
                <Grid item xs={4}>
                    <Box sx={{...fixtureDetailsBetsSectionDisplayInnerBoxStyle, border: "none"}}>
                        <Typography
                            sx={fixtureDetailsBetsSectionDisplayTitleStyle}
                        >
                            {awayTeamName} to win at full time
                        </Typography>
                        <Typography
                            sx={fixtureDetailsBetsSectionDisplayMultiplierStyle}
                        >
                            {wdlftData?.awayBetsMultiplier ? `x${wdlftData?.awayBetsMultiplier.toFixed(2)}` : "unknown"}
                        </Typography>
                    </Box>
                </Grid>
            </Grid>
        </Box>
    );
};