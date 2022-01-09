import { createSlice, createAsyncThunk } from '@reduxjs/toolkit'
import axios from "axios";
axios.defaults.withCredentials = true;

const name = "user"
const namespace = (method) => name+"/"+method;

const initialState = 
{
    userData: {},
    loggedInUser: null,
    isLoggedIn: false,
    ratedBanters: []
}

const login = createAsyncThunk(namespace('login'), async (payload) => {
    const {data} = await axios.post('https://localhost:44310/api/auth/login', payload, {withCredentials: true})
    return data;
})
const register = createAsyncThunk(namespace('register'), async (payload) => {
    const {data} = await axios.post('https://localhost:44310/api/auth/register', payload, {withCredentials: true})
    return data;
})
const logout = createAsyncThunk(namespace('logout'), async () => {
    const {data} = await axios.post('https://localhost:44310/api/auth/logout', {withCredentials: true})
    return data;
})
const getUser = createAsyncThunk(namespace('getUser'), async () => {
    const {data} = await axios.get('https://localhost:44310/api/users/current', {withCredentials: true})
    return data;
})
const userSlice = createSlice({
    name: name,
    initialState,
    reducers: {
    },
    extraReducers: builder => {
      builder
        .addCase(login.fulfilled, (state) => {
          return {...state, isLoggedIn: true};
        })
        .addCase(register.fulfilled, (state) => {
          return {...state, isLoggedIn: true};
        })
        .addCase(logout.fulfilled, () => {
          return {initialState};
        })
        .addCase(getUser.fulfilled, (state, payload) => {
          return {...state, loggedInUser: payload.payload};
        })
    }
})

export {login, register, logout, getUser, userSlice }