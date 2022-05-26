// @flow
import * as React from 'react';
import {
    Box,
    Button,
    CircularProgress,
    Typography
} from "@mui/material";
import {
    loginPageBoxStyle,
    loginPageInnerBoxStyle,
    loginPageStyle, loginPageSubmitBoxStyle, loginPageSubmitButtonStyle,
    loginPageTitleTextStyle
} from "../../styles/app/pages/loginPageStyle";
import {Field, Form, Formik} from "formik";
import {Link, useNavigate} from "react-router-dom";
import * as Yup from "yup";
import {IdentityField} from "../../features/identity/IdentityField";
import {useStore} from "../stores/store";

const RegisterSchema = Yup.object().shape({
    username: Yup.string()
        .min(5, "Username must be at least 5 characters long")
        .max(16, "Username can't be longer than 16 characters")
        .required("Required"),
    email: Yup.string()
        .email("Invalid email")
        .required("Required"),
    password: Yup.string()
        .min(5, "Password must be at least 5 characters long")
        .max(64, "Password can't be longer than 64 characters")
        .required("Required"),
});

type Props = {};
export const RegisterPage = (props: Props) => {

    const store = useStore()
    const navigate = useNavigate()

    return (
        <Box sx={loginPageStyle}>
            <Formik
                initialValues={{username: '', email: '', password: ''}}
                validationSchema={RegisterSchema}
                onSubmit={(values, {setErrors, setSubmitting}) => {
                    store.identityStore.register({
                        userName: values.username,
                        email: values.email,
                        password: values.password
                    })
                        .then(() => {
                            navigate("/fixtures")
                        })
                        .catch(err => {
                            console.log(err.response.data.Errors.UserName)
                            if (err.response.data.Errors.UserName || err.response.data.Errors.Email || err.response.data.Errors.Password)
                                setErrors({
                                    username: err.response.data.Errors.UserName,
                                    email: err.response.data.Errors.Email,
                                    password: err.response.data.Errors.Password
                                })
                            else
                                setErrors({
                                    username: "Something went wrong",
                                    email: "Something went wrong",
                                    password: "Something went wrong"
                                })

                        })
                        .finally(() => {
                            setSubmitting(false)
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
                            Register!
                        </Typography>
                        <Box sx={loginPageInnerBoxStyle}>
                            <IdentityField
                                name="Username"
                                error={errors.username || ""}
                                touched={touched.username || false}
                                value={values.username || ""}
                                setValue={(value: string) => {
                                    setValues({...values, username: value})
                                }}
                            />
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
                                type="submit"
                                onClick={submitForm}
                                variant="outlined"
                                color="secondary"
                                size="large"
                                sx={loginPageSubmitButtonStyle}
                            >
                                {isSubmitting ? <CircularProgress size="3rem" color="secondary"/> : <>Register</>}
                            </Button>
                            <Typography
                                component={Link}
                                to="/login"
                                variant="h4"
                                sx={{my: "1rem", color: "yellow"}}
                            >
                                Login instead
                            </Typography>
                        </Box>
                    </Box>
                )}
            </Formik>
        </Box>
    );
};