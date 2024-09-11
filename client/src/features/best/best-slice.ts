import { createSlice } from '@reduxjs/toolkit'
import { CoffeeItem, Status } from 'types'
import { loadItems } from './best-actions'

export type BestSliceType = {
	status: Status,
	error: string | null
	list: CoffeeItem[],
}

const initialState: BestSliceType = {
	status: 'idle',
	error: null,
	list: []
}

const bestSlice = createSlice({
	name: '@@coffees',
	initialState,
	reducers: {
		clearItems: (state) => {
			state.list = initialState.list
		}
	},
	extraReducers: builder => {
		builder.addCase(loadItems.pending, (state) => {
			state.status = 'loading'
		})
		builder.addCase(loadItems.fulfilled, (state, action) => {
			state.list = action.payload.value.items
			state.status = 'confirmed'
		})
		builder.addCase(loadItems.rejected, (state, action) => {
			state.status = 'rejected'
			state.error = state.error = `${action.error.code}: ${action.error.message}`
		})
	}
})

export const bestReducer = bestSlice.reducer
export const { clearItems } = bestSlice.actions