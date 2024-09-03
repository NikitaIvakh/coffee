import { createSlice, createSelector } from '@reduxjs/toolkit'
import { loadItems } from './best-actions'

const initialState = {
	status: 'idle',
	error: null,
	list: []
}

const bestSlice = createSlice({
	name: '@@coffees',
	initialState,
	reducers: {},
	extraReducers: builder => {
		builder.addCase(loadItems.pending, (state) => {
			state.status = 'loading'
		})
		builder.addCase(loadItems.fulfilled, (state, action) => {
			state.list = action.payload.value
			state.status = 'confirmed'
		})
		builder.addCase(loadItems.rejected, (state, action) => {
			state.error = action.payload.value || action.meta.data
			state.status = 'error'
		})
	}
})

export const bestReducer = bestSlice.reducer

export const selectBestInfo = createSelector(
	[state => state.best],
	coffees => ({
		status: coffees.status,
		error: coffees.error,
		qty: coffees.list.length
	})
)

export const selectAllCoffees = state => state.best.list
