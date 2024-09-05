import { createSelector } from '@reduxjs/toolkit'

export const selectCoffeeInfo = createSelector(
	(state) => state.coffees.status,
	(state) => state.coffees.error,
	(state) => state.coffees.list.length,
	(status, error, qty) => ({ status, error, qty }))

export const selectAllCoffees = (state) => state.coffees.list

export const selectVisibleCoffees = createSelector(
	selectAllCoffees,
	(_, search) => search,
	(_, __, filter) => filter,
	(coffees, search, filter) => {
		const coffeeItems = coffees.items || []
		
		return coffeeItems
			.filter(coffee => coffee.name.toLowerCase().includes(search.toLowerCase()))
			.filter(coffee => filter === 'All' ||  coffee.coffeeType.toLowerCase() === filter.toLowerCase())
	}
)

export const selectCurrentPage = (state) => state.coffees.currentPage
export const selectTotalCount = (state) => state.coffees.totalCount
export const selectPageSize = (state) => state.coffees.pageSize