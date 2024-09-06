import { createSlice } from '@reduxjs/toolkit'
import type { CoffeeById, Status } from '../../types'
import { LoadCoffeeDetails } from './coffee-actions'

export type CoffeeSliceType = {
	status: Status
	error: string | null
	coffee: CoffeeById | null
}

const initialState: CoffeeSliceType = {
	status: 'idle',
	error: null,
	coffee: null
}

const CoffeeSlice = createSlice({
	name: '@@coffee',
	initialState,
	reducers: {
		clearDetails: () => initialState
	},
	extraReducers: builder => {
		builder.addCase(LoadCoffeeDetails.pending, (state) => {
			state.status = 'loading'
		})
		builder.addCase(LoadCoffeeDetails.fulfilled, (state, action) => {
			state.coffee = action.payload
			state.status = 'confirmed'
		})
		builder.addCase(LoadCoffeeDetails.rejected, (state, action) => {
			state.status = 'rejected'
			state.error = `${action.error.code}: ${action.error.message}`
		})
		builder.addDefaultCase(() => {
		})
	}
})

export const coffeeDetailsReducer = CoffeeSlice.reducer
export const { clearDetails } = CoffeeSlice.actions