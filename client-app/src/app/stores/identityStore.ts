import {makeAutoObservable} from "mobx";
import {LoginDto, RegisterDto, UserDto} from "../models/identity";
import agent from "../api/agent";

export default class IdentityStore {

    token: string|null = null;
    user: UserDto|null = null;

    constructor() {
        makeAutoObservable(this)
        this.token = localStorage.getItem("token")
    }

    login = (credentials: LoginDto) => {
        return agent.Identity.login(credentials)
            .then(res => {
                this.saveUserDto(res.data)
            })
    }

    register = (credentials: RegisterDto) => {
        return agent.Identity.register(credentials)
            .then(res => {
                this.saveUserDto(res.data)
            })
    }

    aboutMe = (refreshToken: boolean) => {
        if(!this.token) return
        agent.Identity.aboutMe(refreshToken)
            .then(res => {
                this.saveUserDto(res.data)
            })
    }

    addBalance = () => {
        agent.Identity.addBalance()
            .then(res => {
                this.saveUserDto(res.data)
            })
    }

    saveUserDto = (userDto: UserDto) => {
        this.user = userDto
        if(userDto.token)
        {
            localStorage.setItem("token", userDto.token)
            this.token = userDto.token
        }
    }

    logout = () => {
        this.user = null
        this.token = null
        localStorage.removeItem("token")
    }
}