import { createSlice } from '@reduxjs/toolkit'
import type { Filter } from '../../types'

type ControlsSliceType = {
	search: string
	filter: Filter
}

const initialState: ControlsSliceType = {
	search: '',
	filter: 'All'
}

const ControlsSlice = createSlice({
	name: '@@controls',
	initialState,
	reducers: {
		setSearch: (state, action) => {
			state.search = action.payload
		},
		setFilter: (state, action) => {
			state.filter = action.payload
		},
		clearControls: () => initialState
	}
})

export const controlsReducer = ControlsSlice.reducer
export const { setSearch, setFilter, clearControls } = ControlsSlice.actions