// @flow
import * as React from 'react';
import {RefObject} from 'react';
import {Box, Popover, Typography} from "@mui/material";
import {PlayerDto} from "../../app/models/football/players";

const popoverStyle = {
    pointerEvents: 'none',
    ".MuiPopover-paper": {
        backgroundColor: "primary.main",
        borderRadius: 0,
        borderBottom: "4px solid",
        borderColor: "secondary.main"
    }
}

type Props = {
    color: string
    borderColor: string
    player: PlayerDto
    container: RefObject<HTMLElement>
};
export const FixtureDetailsDataSectionPlayerIndicator = ({color, borderColor, player, container}: Props) => {

    const style = {
        borderRadius: "50%",
        border: `2px solid #${borderColor}`,
        backgroundColor: `#${color}`,
        width: "5rem",
        height: "5rem",
        zIndex: 1,
        '&:hover': {
            cursor: "pointer",
        },
        mx: "auto",
    }

    const typographyStyle = {
        color: `#${color}`,
        filter: "invert(100%)",
        fontSize: "3rem",
        textAlign: "center",
        py: "auto",
    }

    const [anchorEl, setAnchorEl] = React.useState<HTMLElement | null>(null);

    const handlePopoverOpen = (event: React.MouseEvent<HTMLElement>) => {
        setAnchorEl(event.currentTarget);
    };

    const handlePopoverClose = () => {
        setAnchorEl(null);
    };

    const open = Boolean(anchorEl);

    return (
        <>
            <Box sx={style}
                 onMouseEnter={handlePopoverOpen}
                 onMouseLeave={handlePopoverClose}
            >
                <Typography
                    sx={typographyStyle}
                >
                    {player.number}
                </Typography>
            </Box>
            <Popover
                id="mouse-over-popover"
                sx={popoverStyle}
                open={open}
                anchorEl={anchorEl}
                container={container.current}
                anchorOrigin={{
                    vertical: 'top',
                    horizontal: 'center',
                }}
                transformOrigin={{
                    vertical: 'bottom',
                    horizontal: 'center',
                }}
                onClose={handlePopoverClose}
            >
                <Typography
                    sx={{p: "1rem 2rem", fontSize: "2rem", color: "secondary.main"}}
                >
                    {player.number} - {player.name} ({player.pos})
                </Typography>
            </Popover>
        </>
    );
};