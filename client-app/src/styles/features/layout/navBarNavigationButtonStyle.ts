export const navBarNavigationButtonStyle = {
    color: "text.secondary",
    fontSize: "2rem",
    borderRadius: "0",
    py: "0",
    mx: "1rem",
    textTransform: "none",
    transition: "all 0.4s",
    "&.active": {
        borderBottom: "5px solid",
        borderColor: "secondary.main",
        transform: "translateY(-0.25em)",
    }
}