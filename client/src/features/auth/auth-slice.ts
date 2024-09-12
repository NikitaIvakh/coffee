import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import type { AuthResponseValues } from '../../types/authForm.ts'

export type AuthSliceType = {
	user: AuthResponseValues | null
	isAuthenticated: boolean
}

const initialState: AuthSliceType = {
	user: null,
	isAuthenticated: false
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
		
		setLogout: (state) => {
			localStorage.clear()
			state.user = null
		}
	}
})

export const auth = authSlice.reducer
export const { setUser, setLogout, setUserAuthenticated, setUserNoAuthenticated } = authSlice.actions
