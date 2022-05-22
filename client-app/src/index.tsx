import React from 'react';
import ReactDOM from 'react-dom/client';
import reportWebVitals from './reportWebVitals';
import "./index.css";
import {ThemeProvider} from "@mui/material";
import theme from "./styles/common/theme";
import {BrowserRouter, Route, Routes} from "react-router-dom";
import {HomePage} from "./app/pages/HomePage";
import {DashboardPage} from "./app/pages/DashboardPage";
import {App} from "./app/layout/App";

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);
root.render(
    <ThemeProvider theme={theme}>
        <BrowserRouter>
            <Routes>
                <Route index element={<HomePage />}/>
                <Route path="/" element={<App/>}>
                    <Route path="fixtures" element={<DashboardPage/>}/>
                    <Route path="mybets" element={<div></div>}/>
                    <Route path="about" element={<div></div>}/>
                </Route>
                {/*<Route path="/about" element={<AboutPage/>}/>*/}
                {/*<Route path="/fixtures/details/:fixtureId" element={<FixtureDetailsPage/>}/>*/}
            </Routes>
        </BrowserRouter>
    </ThemeProvider>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
