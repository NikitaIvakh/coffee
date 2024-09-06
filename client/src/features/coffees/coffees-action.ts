import { createAsyncThunk } from '@reduxjs/toolkit'
import type { Coffee, Extra } from '../../types'
import type { CoffeesSliceType } from './coffees-slice'

export const LoadCoffees = createAsyncThunk<
	Coffee,
	{ search: string, filter: string, page: number, pageSize: number },
	{
		extra: Extra,
		state: { coffees: CoffeesSliceType },
		rejectValue: string
	}
>
(
	'@@coffees',
	async ({ search, filter, page, pageSize }, { extra: { client, api }, rejectWithValue }) => {
		try {
			const response = await client.get(api.ALL_COFFEES(search, filter, page, pageSize))
			return response.data
		} catch (e) {
			if (e instanceof Error)
				return rejectWithValue(e.message)
			
			return rejectWithValue('Unknown error')
		}
	}, {
		condition: (_, { getState }) => {
			const { coffees: { status } } = getState()
			if (status === 'loading') return false
		}
	}
)
