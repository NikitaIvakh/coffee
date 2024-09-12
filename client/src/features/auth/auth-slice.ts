import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import type { AuthResponseValues } from '../../types/authForm.ts'

export type AuthSliceType = {
	user: AuthResponseValues | null
	isAuthenticated: boolean
	isEmailConfirmed: boolean
}

const initialState: AuthSliceType = {
	user: null,
	isAuthenticated: false,
	isEmailConfirmed: false,
}

const authSlice = createSlice({
	name: '@@auth',
	initialState,
	reducers: {
		setUser: (state, action: PayloadAction<AuthResponseValues>) => {
			state.user = { ...action.payload }
			localStorage.setItem('user', JSON.stringify({ ...action.payload }))
		},
		
		setUserAuthenticated: (state) => {
			state.isAuthenticated = true
		},
		
		setUserNoAuthenticated: (state) => {
			state.isAuthenticated = false
		},
		
		setEmailConfirmed: (state) => {
			state.isEmailConfirmed = true
		},
		
		setLogout: (state) => {
			localStorage.clear()
			state.user = null
		}
	}
})

export const auth = authSlice.reducer
export const { setUser, setLogout, setUserAuthenticated, setUserNoAuthenticated, setEmailConfirmed } = authSlice.actions
