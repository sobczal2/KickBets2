import {makeAutoObservable, runInAction} from "mobx";
import {TeamDto} from "../models/football/teams";
import agent from "../api/agent";

export default class TeamStore {

    teamsCache: Map<number, TeamDto|undefined> = new Map<number, TeamDto|undefined>()

    constructor() {
        makeAutoObservable(this)
    }

    getTeamById = async (id: number) => {
        if(this.teamsCache.has(id))
            return this.teamsCache.get(id)
        const res = await agent.Teams.getById(id)
        runInAction(() => {
            this.teamsCache.set(res.data.id, res.data)
        })
        return res.data
    }
}