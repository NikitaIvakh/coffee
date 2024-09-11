import { createSlice, PayloadAction } from '@reduxjs/toolkit'

export type AuthSliceType = {
	id: string | null
	userName: string | null,
	role: string | null,
	jwtToken: string | null
	refreshToken: string | null,
	isAuthenticated: boolean
}

const initialState: AuthSliceType = {
	id: null,
	userName: null,
	role: null,
	jwtToken: null,
	refreshToken: null,
	isAuthenticated: false,
}

const authSlice = createSlice({
	name: '@@auth',
	initialState,
	reducers: {
		setUser: (state, action: PayloadAction<{id: string, userName: string, role: string, jwtToken: string, refreshToken: string}>) => {
			state.id = action.payload.id
			state.role = action.payload.role
			state.userName = action.payload.userName
			state.jwtToken = action.payload.jwtToken
			state.refreshToken = action.payload.refreshToken
			
			localStorage.setItem('user', JSON.stringify({
				id: action.payload.id,
				userName: action.payload.userName,
				role: action.payload.role,
				jwtToken: action.payload.jwtToken,
				refreshToken: action.payload.refreshToken
			}))
		},
		
		setUserAuthenticated: (state) => {
			state.isAuthenticated = true
		},
		
		setUserNoAuthenticated: (state) => {
			state.isAuthenticated = false
		},
		
		setLogout: (state) => {
			localStorage.clear()
			state.id = null
			state.userName = null
			state.jwtToken = null
			state.refreshToken = null
		}
	}
})

export const auth = authSlice.reducer
export const { setUser, setLogout, setUserAuthenticated, setUserNoAuthenticated } = authSlice.actions
