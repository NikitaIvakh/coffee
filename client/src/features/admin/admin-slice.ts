import { createSlice } from '@reduxjs/toolkit'
import type { Status } from '../../types'
import { createNewCoffee } from './admin-action'

export type AdminSliceType = {
	status: Status,
	coffeeId: string,
	error: string | null
}

const initialState: AdminSliceType = {
	status: 'idle',
	coffeeId: '',
	error: null
}

const AdminSlice = createSlice({
	name: '@@admin',
	initialState,
	reducers: {
		clearForm: () => initialState
	},
	extraReducers: builder => {
		builder.addCase(createNewCoffee.pending, state => {
			state.status = "loading"
		})
		builder.addCase(createNewCoffee.fulfilled, (state, action) => {
			state.coffeeId = action.payload
			state.status = "confirmed"
		})
		builder.addCase(createNewCoffee.rejected, (state, action) => {
			state.error = action.payload || "Unknown error"
			state.status="rejected"
		})
		builder.addDefaultCase(() => {})
	}
})

export const admin = AdminSlice.reducer
export const {clearForm} = AdminSlice.actions