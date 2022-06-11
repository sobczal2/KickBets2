// @flow
import * as React from 'react';
import {useEffect, useState} from 'react';
import {
    Box,
    Button,
    CircularProgress,
    Dialog,
    DialogContent,
    DialogTitle,
    FormControl,
    FormControlLabel,
    FormLabel,
    Grid,
    Radio,
    RadioGroup,
    TextField,
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
import agent from "../../app/api/agent";
import dayjs from "dayjs";
import {useStore} from "../../app/stores/store";
import {WdlftBetsDataDto} from "../../app/models/bets/wdlftBets";
import {toast} from "react-toastify";

type Props = {
    fixture?: FixtureDto
};
export const FixtureDetailsBetsSectionWdlftDisplay = ({fixture}: Props) => {

    const [wdlftData, setWdlftData] = useState<WdlftBetsDataDto | undefined>(undefined)
    const [homeTeamName, setHomeTeamName] = useState<string | undefined>(undefined)
    const [awayTeamName, setAwayTeamName] = useState<string | undefined>(undefined)

    const [modalVisible, setModalVisible] = useState(false)

    const [side, setSide] = useState<"home" | "draw" | "away">("home")
    const [value, setValue] = useState<string>("10")
    const [submitting, setSubmitting] = useState(false)

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
        >
            <Dialog
                open={modalVisible}
                onClose={() => setModalVisible(false)}
            >
                <DialogTitle
                    sx={{fontSize: "2rem", color: "secondary.main"}}
                >Bet: Score at full time</DialogTitle>
                <DialogContent
                >
                    <Box
                        sx={{display: "flex", flexDirection: "column"}}
                    >
                        <FormControl>
                            <FormLabel
                                sx={{fontSize: "1.5rem", color: "secondary.main"}}
                            >Side</FormLabel>
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
                            id="wdlft-input-field"
                            value={value}
                            onChange={e => {
                                if (/^([0-9]+(\.[0-9]+)?|)$/.test(e.target.value))
                                    setValue(e.target.value)
                            }}
                            onBlur={() => {
                                if (value === "")
                                    setValue("10")
                            }}
                            variant="standard"
                        />
                    </Box>
                    <Box
                        sx={{
                            display: "flex",
                            width: "100%",
                            flexDirection: "row",
                            justifyContent: "space-between",
                            mt: "1rem"
                        }}
                    >
                        <Button
                            id="wdlft-modal-submit-button"
                            sx={{fontSize: "1.5rem", color: "secondary.main"}}
                            onClick={() => {
                                if (!store.identityStore.user) {
                                    toast("Login first!", {type: "error"})
                                    return
                                }
                                setSubmitting(true)
                                agent.Bets.createWdlftBet(fixture!.id, parseFloat(value), side)
                                    .then(res => {
                                        setModalVisible(false)
                                        store.identityStore.aboutMe(false)
                                    })
                                    .catch(err => {
                                        toast(err.response.data.Errors.Value, {type: "error"})
                                    })
                                    .finally(() => {
                                        setSubmitting(false)
                                    })
                            }}
                        >
                            {submitting ? <CircularProgress color="secondary" size="1.5rem"/> : "Submit"}
                        </Button>
                        <Button
                            onClick={() => {
                                setModalVisible(false)
                            }

                            }
                            sx={{fontSize: "1.5rem", color: "red"}}
                        >
                            Cancel
                        </Button>
                    </Box>

                </DialogContent>
            </Dialog>
            <Box
                id="wdlft-panel"
                onClick={() => {
                    if (available) setModalVisible(true)
                }}
            >
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
        </Box>
    );
};