import FixtureStore from "./fixtureStore";
import TeamStore from "./teamStore";
import {createContext, useContext} from "react";
import LeagueStore from "./leagueStore";

interface Store {
    fixtureStore: FixtureStore,
    teamStore: TeamStore,
    leagueStore: LeagueStore
}

export const store: Store = {
    fixtureStore: new FixtureStore(),
    teamStore: new TeamStore(),
    leagueStore: new LeagueStore()
}

export const StoreContext = createContext(store)

export const useStore = () => {
    return useContext(StoreContext)
}