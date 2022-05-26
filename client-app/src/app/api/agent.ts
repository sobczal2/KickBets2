import axios, {AxiosResponse} from "axios";
import {PaginatedRequestData, PaginatedResponse} from "../models/common/pagination";
import {FixtureDto, FixtureListParams} from "../models/football/fixtures";
import dayjs from "dayjs";
import {LeagueDto} from "../models/football/leagues";
import {LineupDto} from "../models/football/lineups";
import {PlayerDto} from "../models/football/players";
import {ScoreDto} from "../models/football/scores";
import {StatisticDto} from "../models/football/statistics";
import {StatusDto} from "../models/football/statuses";
import {TeamDto} from "../models/football/teams";
import {LoginDto, RegisterDto, UserDto} from "../models/identity";
import {store} from "../stores/store";

const sleep = (delay: number) => {
    return new Promise((resolve) => {
        setTimeout(resolve, delay)
    })
}

axios.defaults.baseURL = 'https://localhost:7103/api'

axios.interceptors.response.use(async res => {
    try {
        await sleep(1000)
        return res;
    } catch (error) {
        console.log(error)
        return Promise.reject(error)
    }
})

axios.interceptors.request.use(req => {
    const token = store.identityStore.token
    if(token) req.headers!.Authorization = `Bearer ${token}`
    return req
})

const requests = {
    get: <T>(url: string, params: {}) => axios.get<T>(url, {params: params}),
    post: <T>(url: string, body: {}) => axios.post<T>(url, body),
    put: <T>(url: string, body: {}) => axios.put<T>(url, body),
    del: <T>(url: string) => axios.delete<T>(url),
}

const Fixtures = {
    list: (paginatedRequestData: PaginatedRequestData, fixtureListParams: FixtureListParams) =>
        requests.get<PaginatedResponse<FixtureDto>>("/fixtures", {
            pageSize: paginatedRequestData.pageSize,
            currentPage: paginatedRequestData.currentPage,
            type: fixtureListParams.type,
            leagueId: fixtureListParams.leagueId
        })
            .then(res => {
                res.data.items.forEach(f => {
                    f.date = dayjs(f.date)
                })
                return res
            }),
    getById: (fixtureId: number) =>
        requests.get<FixtureDto>(`/fixtures/${fixtureId}`, {}),
}

const Leagues = {
    list: (paginatedRequestData: PaginatedRequestData) =>
        requests.get<PaginatedResponse<LeagueDto>>("/leagues", {
            pageSize: paginatedRequestData.pageSize,
            currentPage: paginatedRequestData.currentPage
        }),
    getById: (leagueId: number) =>
        requests.get<LeagueDto>(`/leagues/${leagueId}`, {}),
}

const Lineups = {
    getById: (lineupId: number) =>
        requests.get<LineupDto>(`/lineups/${lineupId}`, {}),
    getPlayersByLineupId: (lineupId: number) =>
        requests.get<PlayerDto[]>(`/lineups/${lineupId}/players`, {}),
}

const Scores = {
    getById: (scoreId: number) =>
        requests.get<ScoreDto>(`/scores/${scoreId}`, {}),
}

const Statistics = {
    getById: (statisticId: number) =>
        requests.get<StatisticDto>(`/statistics/${statisticId}`, {}),
}

const Statuses = {
    getById: (statusId: number) =>
        requests.get<StatusDto>(`/statuses/${statusId}`, {}),
}

const Teams = {
    getById: (teamsId: number) =>
        requests.get<TeamDto>(`/teams/${teamsId}`, {}),
}

const Identity = {
    login: (credentials: LoginDto) =>
        requests.post<UserDto>(`/identity/login`, credentials),
    register: (credentials: RegisterDto) =>
        requests.post<UserDto>(`/identity/register`, credentials),
    aboutMe: (refreshToken: boolean) =>
        requests.get<UserDto>("/identity", {refreshToken: refreshToken}),
    addBalance: () =>
        requests.post<UserDto>("/identity/addbalance", {}),
}

const agent = {
    Fixtures,
    Leagues,
    Lineups,
    Scores,
    Statistics,
    Statuses,
    Teams,
    Identity,
}

export default agent