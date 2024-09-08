import { createSelector } from '@reduxjs/toolkit'
import type { RootState } from 'store/store'
import type { CoffeeItem } from 'types'

export const selectCoffeeInfo = createSelector(
	(state: RootState) => state.coffees.status,
	(state: RootState) => state.coffees.errors,
	(state: RootState) => state.coffees.list.length,
	(status, error, qty) => ({ status, error, qty }))

export const selectAllCoffees = (state: RootState) => state.coffees.list

export const selectVisibleCoffees = createSelector(
	selectAllCoffees,
	(_, search) => search,
	(_, __, filter) => filter,
	(coffees: CoffeeItem[], search: string, filter: string) => {
		const coffeeItems = coffees || []
		
		return coffeeItems
			.filter(coffee => coffee.name.toLowerCase().includes(search.toLowerCase()))
			.filter(coffee => filter === 'All' || coffee.coffeeType.toLowerCase() === filter.toLowerCase())
	}
)

export const selectCurrentPage = (state: RootState) => state.coffees.currentPage
export const selectTotalCount = (state: RootState) => state.coffees.totalCount
export const selectPageSize = (state: RootState) => state.coffees.pageSize