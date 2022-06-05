import React, {useEffect} from 'react';
import {NavBar} from "./NavBar";
import {Outlet} from "react-router-dom";
import {useStore} from "../stores/store";
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

type Props = {};

export const App = (props: Props) => {

    const store = useStore()

    useEffect(() => {
        if(!store.identityStore.user)
            store.identityStore.aboutMe(true)
    }, [])

    return (
        <>
            <NavBar/>
            <Outlet/>
            <ToastContainer position="bottom-right"/>
        </>
    );
}