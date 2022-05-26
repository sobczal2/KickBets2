// @flow
import * as React from 'react';
import {FilledInput, FormControl, FormHelperText, InputLabel} from "@mui/material";
import {
    identityFieldHelperStyle,
    identityFieldInputStyle,
    identityFieldLabelStyle,
    identityFieldStyle
} from "../../styles/features/identity/identityFieldStyle";

type Props = {
    name: string
    error: string
    touched: boolean
    value: string
    setValue: (value: string) => void
    password?: boolean
};
export const IdentityField = ({name, error, touched, value, setValue, password}: Props) => {
    return (
        <FormControl
            error={!!error && touched}
            variant="outlined"
            size="medium"
            sx={identityFieldStyle}
        >
            <InputLabel
                htmlFor={name}
                color="secondary"
                sx={identityFieldLabelStyle}
            >
                {name}
            </InputLabel>
            <FilledInput
                id={name}
                value={value}
                type={password ? "password" : ""}
                onChange={event => setValue(event.target.value)}
                color="secondary"
                aria-describedby="username-login-error"
                sx={identityFieldInputStyle}
            />
            <FormHelperText
                id="username-login-error"
                sx={identityFieldHelperStyle}
            >
                {touched && error}
            </FormHelperText>
        </FormControl>
    );
};