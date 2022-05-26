import FixtureStore from "./fixtureStore";
import TeamStore from "./teamStore";
import {createContext, useContext} from "react";
import LeagueStore from "./leagueStore";
import IdentityStore from "./identityStore";

interface Store {
    fixtureStore: FixtureStore,
    teamStore: TeamStore,
    leagueStore: LeagueStore,
    identityStore: IdentityStore
}

export const store: Store = {
    fixtureStore: new FixtureStore(),
    teamStore: new TeamStore(),
    leagueStore: new LeagueStore(),
    identityStore: new IdentityStore()
}

export const StoreContext = createContext(store)

export const useStore = () => {
    return useContext(StoreContext)
}