import React, {useEffect} from 'react';
import {NavBar} from "./NavBar";
import {Outlet} from "react-router-dom";

type Props = {
};

export const App = (props: Props) => {
  return (
      <>
          <NavBar/>
          <Outlet/>
      </>
  );
}