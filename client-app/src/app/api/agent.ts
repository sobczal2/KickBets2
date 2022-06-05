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
import {VenueDto} from "../models/football/venues";
import {BaseBetDto, BetsDataDto} from "../models/bets/bets";

axios.defaults.baseURL = 'https://localhost:7103/api'

axios.interceptors.request.use(req => {
    const token = store.identityStore.token
    if(token) req.headers!.Authorization = `Bearer ${token}`
    return req
})

const requests = {
    get: <T>(url: string, params: {}) => axios.get<T>(url, {params: params}),
    post: <T>(url: string, body: {}) => axios.post<T>(url, body),
    postWithParams: <T>(url: string, body: {}, params: {}) => axios.post<T>(url, body, {params: params}),
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
        requests.get<FixtureDto>(`/fixtures/${fixtureId}`, {})
            .then(res => {
                res.data.date = dayjs(res.data.date)
                return res
            }),
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
    getById: (teamId: number) =>
        requests.get<TeamDto>(`/teams/${teamId}`, {}),
}

const Venues = {
    getById: (venueId: number) =>
        requests.get<VenueDto>(`/venues/${venueId}`, {}),
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

const Bets = {
    createWdlhtBet: (fixtureId: number, value: number, wdlhtSide: "home"|"away"|"draw") =>
        requests.post("/bets/wdlht", {fixtureId: fixtureId, value: value, wdlhtSide: wdlhtSide}),
    createWdlftBet: (fixtureId: number, value: number, wdlftSide: "home"|"away"|"draw") =>
        requests.post("/bets/wdlft", {fixtureId: fixtureId, value: value, wdlftSide: wdlftSide}),
    getBetsData: (betsDataId: number) =>
        requests.get<BetsDataDto>(`/betsdata/${betsDataId}`, {}),
    getMyBets: (paginatedRequestData: PaginatedRequestData) =>
        requests.get<PaginatedResponse<BaseBetDto>>("/bets", {
            pageSize: paginatedRequestData.pageSize,
            currentPage: paginatedRequestData.currentPage,
        })
}

const agent = {
    Fixtures,
    Leagues,
    Lineups,
    Scores,
    Statistics,
    Statuses,
    Teams,
    Venues,
    Identity,
    Bets,
}

export default agent