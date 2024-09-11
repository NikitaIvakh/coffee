import { createAsyncThunk } from '@reduxjs/toolkit'
import type { CoffeeById, Extra } from 'types'
import type { CoffeeSliceType } from './coffee-slice'

export const LoadCoffeeDetails = createAsyncThunk<
	CoffeeById,
	string,
	{
		extra: Extra,
		state: { coffeeDetails: CoffeeSliceType },
		rejectValue: string
	}
>(
	'@@coffee',
	async (id, { extra: { client, api }, rejectWithValue }) => {
		try {
			const user = JSON.parse(localStorage.getItem('user') as '{}')
			if (!user) return rejectWithValue('User data is missing')
			const response = await client.get(api.GET__COFFEE_BY_ID(id), {
				headers: {
					'Authorization': `Bearer ${user.jwtToken}`
				}
			})
			return response.data.value
		} catch (e) {
			if (e instanceof Error)
				return rejectWithValue(e.message)
			
			return rejectWithValue('Unknown error')
		}
	}, {
		condition: (_, { getState }) => {
			const { coffeeDetails: { status } } = getState()
			if (status === 'loading') return false
		}
	}
)