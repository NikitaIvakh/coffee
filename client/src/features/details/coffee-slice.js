import { createSlice } from '@reduxjs/toolkit'
import { LoadCoffeeDetails } from './coffee-actions'

const initialState = {
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
			state.coffee = action.payload.value
			state.status = 'confirmed'
		})
		builder.addCase(LoadCoffeeDetails.rejected, (state, action) => {
			state.coffee = action.payload || action.meta.data
			state.error = `${action.error.code}: ${action.error.message}`
			state.status = 'error'
		})
		builder.addDefaultCase(() => {
		})
	}
})

export const coffeeDetailsReducer = CoffeeSlice.reducer
export const { clearDetails } = CoffeeSlice.actions