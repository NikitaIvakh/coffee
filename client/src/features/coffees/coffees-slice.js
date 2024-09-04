import { createSlice } from '@reduxjs/toolkit'
import { LoadCoffees } from './coffees-action'

const initialState = {
	status: 'idle',
	errors: null,
	list: []
}

const CoffeesSlice = createSlice({
	name: '@@coffees',
	initialState,
	reducers: {},
	extraReducers: builder => {
		builder.addCase(LoadCoffees.pending, (state) => {
			state.status = 'loading'
		})
		builder.addCase(LoadCoffees.fulfilled, (state, action) => {
			state.list = action.payload.value
			state.status = 'confirmed'
		})
		builder.addCase(LoadCoffees.rejected, (state, action) => {
			state.list = action.payload || action.meta.data
			state.errors = action.payload
		})
	}
})

export const coffeeList = CoffeesSlice.reducer
