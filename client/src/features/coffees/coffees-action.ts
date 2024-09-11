import { createAsyncThunk } from '@reduxjs/toolkit'
import type { Coffee, Extra } from 'types'
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
			const user = JSON.parse(localStorage.getItem('user') as '{}')
			if (!user) return rejectWithValue('User data is missing')
			const response = await client.get(api.ALL_COFFEES(search, filter, page, pageSize), {
				headers: {
					'Authorization': `Bearer ${user.jwtToken}`
				}
			})
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
