import { createAsyncThunk } from '@reduxjs/toolkit'
import { Coffee, Extra } from 'types'
import { BestSliceType } from './best-slice'

export const loadItems = createAsyncThunk<
	Coffee,
	undefined,
	{
		extra: Extra
		state: { best: BestSliceType }
		rejectedValue: string
	}
>(
	'@@coffees/load-coffees',
	async (_, { extra: { client, api }, rejectWithValue }) => {
		try {
			const response = await client.get(api.ALL_COFFEES_WITH_LIMIT)
			return response.data
		} catch (e) {
			if (e instanceof Error)
				return rejectWithValue(e.message)
			
			return rejectWithValue('Unknown error')
		}
	},
	{
		condition: (_, { getState }) => {
			const { best: { status } } = getState()
			if (status === 'loading')
				return false
		}
	}
)
