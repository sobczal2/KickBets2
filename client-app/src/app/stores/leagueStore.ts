import {LeagueDto} from "../models/football/leagues";
import {makeAutoObservable, runInAction} from "mobx";
import agent from "../api/agent";

export default class LeagueStore {
    leaguesCache: Map<number, LeagueDto|undefined> = new Map<number, LeagueDto|undefined>()

    constructor() {
        makeAutoObservable(this)
    }

    getLeagueById = async (id: number) => {
        if(this.leaguesCache.has(id))
            return this.leaguesCache.get(id)
        const res = await agent.Leagues.getById(id)
        runInAction(() => {
            this.leaguesCache.set(res.data.id, res.data)
        })
        return res.data
    }
}