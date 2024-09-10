import { createSlice, PayloadAction } from '@reduxjs/toolkit'

export type AuthSliceType = {
	id: string | null
	userName: string | null
	jwtToken: string | null
	refreshToken: string | null
}

const initialState: AuthSliceType = {
	id: null,
	userName: null,
	jwtToken: null,
	refreshToken: null
}

const authSlice = createSlice({
	name: '@@auth',
	initialState,
	reducers: {
		setUser: (state, action: PayloadAction<{id: string, userName: string, jwtToken: string, refreshToken: string}>) => {
			state.id = action.payload.id
			state.userName = action.payload.userName
			state.jwtToken = action.payload.jwtToken
			state.refreshToken = action.payload.refreshToken
			
			localStorage.setItem('user', JSON.stringify({
				id: action.payload.id,
				userName: action.payload.userName,
				jwtToken: action.payload.jwtToken,
				refreshToken: action.payload.refreshToken
			}))
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
export const { setUser, setLogout } = authSlice.actions
