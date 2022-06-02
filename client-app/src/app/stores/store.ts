import TeamStore from "./teamStore";
import {createContext, useContext} from "react";
import LeagueStore from "./leagueStore";
import IdentityStore from "./identityStore";

interface Store {
    teamStore: TeamStore,
    leagueStore: LeagueStore,
    identityStore: IdentityStore
}

export const store: Store = {
    teamStore: new TeamStore(),
    leagueStore: new LeagueStore(),
    identityStore: new IdentityStore()
}

export const StoreContext = createContext(store)

export const useStore = () => {
    return useContext(StoreContext)
}