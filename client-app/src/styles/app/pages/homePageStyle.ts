export const homePageStyle = {
    height: "100vh",
    backgroundImage: "url(/images/homeBackgroundImage.jpg)",
    backgroundSize: "cover",
    backgroundPosition: "center",
    backgroundRepeat: "no-repeat",
    display: "flex",
    flexDirection: "column",
    justifyContent: "center",
    alignItems: "center"
}

export const homePageTitleStyle = {
    fontSize: "15rem",
    fontWeight: "700",
    color: "secondary.main",
    my: "5rem",
    letterSpacing: "2rem",
    fontStyle: "italic"
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