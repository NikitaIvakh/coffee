import { createSlice } from '@reduxjs/toolkit'
import { createNewCoffee, updateCoffee } from './admin-action'

export type AdminSliceType = {
	status: 'idle' | 'loading' | 'confirmed' | 'rejected',
	coffeeId: string | undefined,
	error: string | null
}

const initialState: AdminSliceType = {
	status: 'idle',
	coffeeId: undefined,
	error: null
}

const AdminSlice = createSlice({
	name: 'admin',
	initialState,
	reducers: {
		clearForm: () => initialState
	},
	extraReducers: builder => {
		builder
			.addCase(createNewCoffee.pending, state => {
				state.status = 'loading'
			})
			.addCase(createNewCoffee.fulfilled, (state, action) => {
				state.coffeeId = action.payload
				state.status = 'confirmed'
			})
			.addCase(createNewCoffee.rejected, (state, action) => {
				state.error = action.payload || 'Unknown error'
				state.status = 'rejected'
			})
			.addCase(updateCoffee.pending, state => {
				state.status = 'loading'
			})
			.addCase(updateCoffee.fulfilled, state => {
				state.status = 'confirmed'
			})
			.addCase(updateCoffee.rejected, (state, action) => {
				state.error = action.payload || 'Unknown error'
				state.status = 'rejected'
			})
	}
})

export const admin = AdminSlice.reducer
export const { clearForm } = AdminSlice.actions
