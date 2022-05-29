export const homePageStyle = {
    height: "100vh",
    backgroundImage: "url(/images/homeBackgroundImage.jpg)",
    backgroundSize: "cover",
    backgroundPosition: "center",
    backgroundRepeat: "no-repeat",
    display: "flex",
    flexDirection: "column",
    justifyContent: "center",
    alignItems: "center",
}

export const homePageTitleStyle = {
    fontSize: "15rem",
    fontWeight: "700",
    color: "secondary.main",
    my: "5rem",
    letterSpacing: "3rem",
    fontStyle: "italic",
    position: "relative",
    display: "flex",
    "@keyframes bounce": {
        "0%": {transform: "scale(1,1) translateY(0)"},
        "10%": {transform: "scale(1.1,.9) translateY(0) rotate(0deg)"},
        "30%": {transform: "scale(.9,1.1) translateY(-55px)"},
        "50%": {transform: "scale(1.05,.95) translateY(0)"},
        "58%": {transform: "scale(1,1) translateY(-7px)"},
        "65%": {transform: "scale(1,1) translateY(0)"},
        "100%": {transform: "scale(1,1) translateY(0)"}
    },
    "span": {
        animation: "bounce 2s ease infinite",
    },
    ".l2": {
        animation: "bounce 2s ease infinite .2s",
    },
    ".l3": {
        animation: "bounce 2s ease infinite .4s",
    },
    ".l4": {
        animation: "bounce 2s ease infinite .6s",
    },
    ".l5": {
        animation: "bounce 2s ease infinite .8s",
    },
    ".l6": {
        animation: "bounce 2s ease infinite 1s",
    },
    ".l7": {
        animation: "bounce 2s ease infinite 1.2s",
    },
    ".l8": {
        animation: "bounce 2s ease infinite 1.4s",
    }
}

export const homePageButtonStyle = {
    fontSize: "6rem",
    color: "secondary.main",
    p: "1rem 2rem",
    m: "1rem 1rem",
    backgroundColor: "transparent",
    border: "2px solid",
    borderColor: "secondary.main",
    transition: "transform 0.5s, box-shadow 0.1s, background 0.5s",
    "&:hover, &:focus": {
        boxShadow: "0 0.5em 0.5em -0.4em",
        transform: "translateY(-0.25em)",
        backgroundColor: "primary.main"
    }
}