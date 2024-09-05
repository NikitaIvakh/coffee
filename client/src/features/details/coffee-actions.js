import { createAsyncThunk } from '@reduxjs/toolkit'

export const LoadCoffeeDetails = createAsyncThunk(
	'@@coffee',
	async (id, { extra: { client, api } }) => {
		const response = await client.get(api.GET__COFFEE_BY_ID(id))
		return response.data
	}, {
		condition: (id, { getState }) => {
			const { coffeeDetails: { status } } = getState()
			if (status === 'loading') return false
		}
	}
)