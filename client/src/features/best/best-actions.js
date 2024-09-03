import { createAsyncThunk } from '@reduxjs/toolkit'

export const loadItems = createAsyncThunk(
	'@@coffees/load-coffees',
	async (_, { extra: { client, api } }) => {
		const response = await client.get(api.ALL_COFFEES_WITH_OFFSET)
		return response.data
	},
	{
		condition: (_, { getState }) => {
			const { best: { status } } = getState()
			if (status === 'loading') return false
		}
	}
)
