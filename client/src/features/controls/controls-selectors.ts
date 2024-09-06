import type { RootState } from '../../store/store'

export const selectSearch = (state: RootState) => state.controls.search
export const selectFilters = (state: RootState) => state.controls.filter
export const selectControls = (state: RootState) => state.controls