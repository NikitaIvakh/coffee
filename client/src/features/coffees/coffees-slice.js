import { createSlice } from '@reduxjs/toolkit'
import { LoadCoffees } from './coffees-action'

const initialState = {
	status: 'idle',
	errors: null,
	list: [],
	currentPage: 1,
	pageSize: 6,
	totalCount: 0
}

const CoffeesSlice = createSlice({
	name: '@@coffees',
	initialState,
	reducers: {
		setCurrentPage: (state, action) => {
			state.currentPage = action.payload
		}
	},
	extraReducers: builder => {
		builder.addCase(LoadCoffees.pending, (state) => {
			state.status = 'loading'
		})
		builder.addCase(LoadCoffees.fulfilled, (state, action) => {
			state.list = action.payload.value
			state.status = 'confirmed'
			state.totalCount = action.payload.value.totalCount
		})
		builder.addCase(LoadCoffees.rejected, (state, action) => {
			state.list = []
			state.status = 'error'
			state.errors = `${action.error.code}: ${action.error.message}`
		})
	}
})

export const coffeeList = CoffeesSlice.reducer
export const { setCurrentPage } = CoffeesSlice.actions
