import { createSelector } from '@reduxjs/toolkit'
import type { RootState } from 'store/store'

export const selectBestInfo = createSelector(
	(state => state.best),
	coffees => ({
		status: coffees.status,
		error: coffees.error,
		qty: coffees.list.length
	})
)

export const selectAllCoffees = (state: RootState) => state.best.list