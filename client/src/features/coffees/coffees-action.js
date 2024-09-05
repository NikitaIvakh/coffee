import { createAsyncThunk } from '@reduxjs/toolkit'

export const LoadCoffees = createAsyncThunk(
	'@@coffees',
	async (_, { extra: { client, api } }) => {
		const response = await client.get(api.ALL_COFFEES)
		return response.data
	}, {
		condition: (_, { getState }) => {
			const { coffees: { status } } = getState()
			if (status === 'loading') return false
		}
	}
)