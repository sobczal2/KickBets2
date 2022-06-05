// @flow
import * as React from 'react';
import {Box, Typography} from "@mui/material";
import {aboutPageStyle} from "../../styles/app/pages/aboutPageStyle";
import Particles from "react-tsparticles";
import {loadFull} from "tsparticles";

type Props = {

};
export const AboutPage = (props: Props) => {

    return (
        <Box sx={aboutPageStyle}>
            <Typography
                sx={{color: "#FFFFFF", position: "absolute", top: "20rem", left: "0", width: "100%", zIndex: "100000", fontSize: "4rem", textAlign: "center", fontWeight: "700"}}
            >
                KickBets is a simple app that makes it easy for
                <br/>
                everyone to start betting without spending real
                <br/>
                money. I created it for an individual project
                <br/>
                while studying at Warsaw University of Technology.
                <br/>
                Just register and give it a try!

            </Typography>
            <Particles
                id="tsparticles"
                init={async main => await loadFull(main)}
                options={{
                    background: {
                        color: {
                            value: "#303030",
                        },
                    },
                    fpsLimit: 120,
                    interactivity: {
                        events: {
                            onClick: {
                                enable: true,
                                mode: "push",
                            },
                            onHover: {
                                enable: true,
                                mode: "repulse",
                            },
                            resize: true,
                        },
                        modes: {
                            push: {
                                quantity: 4,
                            },
                            repulse: {
                                distance: 200,
                                duration: 0.4,
                            },
                        },
                    },
                    particles: {
                        color: {
                            value: "#fdee30",
                        },
                        links: {
                            color: "#fdee30",
                            distance: 150,
                            enable: true,
                            opacity: 0.5,
                            width: 1,
                        },
                        collisions: {
                            enable: true,
                        },
                        move: {
                            direction: "none",
                            enable: true,
                            outModes: {
                                default: "bounce",
                            },
                            random: false,
                            speed: 2,
                            straight: false,
                        },
                        number: {
                            density: {
                                enable: true,
                                area: 800,
                            },
                            value: 80,
                        },
                        opacity: {
                            value: 0.5,
                        },
                        shape: {
                            type: "circle",
                        },
                        size: {
                            value: { min: 1, max: 5 },
                        },
                    },
                    detectRetina: true,
                }}
            />
        </Box>
    );
};