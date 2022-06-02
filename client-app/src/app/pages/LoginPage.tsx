// @flow
import * as React from 'react';
import {
    Box,
    Button,
    CircularProgress,
    FilledInput,
    FormControl,
    FormHelperText,
    Input,
    InputLabel,
    Typography
} from "@mui/material";
import {
    loginPageBoxStyle,
    loginPageStyle,
    loginPageTitleTextStyle,
    loginPageSubmitButtonStyle, loginPageInnerBoxStyle, loginPageSubmitBoxStyle
} from "../../styles/app/pages/loginPageStyle";
import {Formik} from "formik";
import * as Yup from "yup";
import {Link, useNavigate} from "react-router-dom";
import {useEffect} from "react";
import {IdentityField} from "../../features/identity/IdentityField";
import agent from "../api/agent";
import {useStore} from "../stores/store";


const sleep = (delay: number) => {
    return new Promise((resolve) => {
        setTimeout(resolve, delay)
    })
}

const LoginSchema = Yup.object().shape({
    email: Yup.string()
        .email("Invalid email")
        .required('Required'),
    password: Yup.string()
        .required('Required'),
});

type Props = {};
export const LoginPage = (props: Props) => {

    const navigate = useNavigate()
    const store = useStore()

    return (
        <Box sx={loginPageStyle}>
            <Formik
                initialValues={{email: '', password: ''}}
                validationSchema={LoginSchema}
                onSubmit={(values, {setErrors, setSubmitting}) => {
                    setSubmitting(true)
                    sleep(1000).then(() => {
                        store.identityStore.login({email: values.email, password: values.password})
                            .then(() => {
                                navigate("/fixtures")
                            })
                            .catch(err => {
                                if (err.response.data.Errors.Email || err.response.data.Errors.Password)
                                    setErrors({
                                        email: err.response.data.Errors.Email,
                                        password: err.response.data.Errors.Password
                                    })

                                else
                                    setErrors({email: "Something went wrong", password: "Something went wrong"})

                            })
                            .finally(() => {
                                setSubmitting(false)
                            })
                    })

                }}
            >
                {({values, errors, touched, setValues, isSubmitting, submitForm}) => (
                    <Box
                        sx={loginPageBoxStyle}
                        onKeyDown={(e) => {
                            if (e.key === 'Enter') {
                                submitForm();
                            }
                        }}
                    >
                        <Typography
                            variant="h3"
                            sx={loginPageTitleTextStyle}
                        >
                            Login!
                        </Typography>
                        <Box sx={loginPageInnerBoxStyle}>
                            <IdentityField
                                name="Email"
                                error={errors.email || ""}
                                touched={touched.email || false}
                                value={values.email || ""}
                                setValue={(value: string) => {
                                    setValues({...values, email: value})
                                }}
                            />
                            <IdentityField
                                name="Password"
                                password
                                error={errors.password || ""}
                                touched={touched.password || false}
                                value={values.password || ""}
                                setValue={(value: string) => {
                                    setValues({...values, password: value})
                                }}
                            />
                        </Box>
                        <Box sx={loginPageSubmitBoxStyle}>
                            <Button
                                id="loginSubmitButton"
                                type="submit"
                                onClick={submitForm}
                                variant="outlined"
                                color="secondary"
                                size="large"
                                sx={loginPageSubmitButtonStyle}
                            >
                                {isSubmitting ? <CircularProgress size="3rem" color="secondary"/> : <>Login</>}
                            </Button>
                            <Typography
                                component={Link}
                                to="/register"
                                variant="h4"
                                sx={{my: "1rem", color: "yellow"}}
                            >
                                Register instead
                            </Typography>
                        </Box>
                    </Box>
                )}
            </Formik>
        </Box>
    );
};