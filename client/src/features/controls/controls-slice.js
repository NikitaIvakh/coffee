import { createSlice } from '@reduxjs/toolkit'

const initialState = {
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