import { createAsyncThunk } from '@reduxjs/toolkit'
import { Extra } from '../../types'
import { AdminSliceType } from './admin-slice'

export const createNewCoffee = createAsyncThunk<
	string,
	FormData,
	{
		extra: Extra,
		state: { adminPanel: AdminSliceType },
		rejectValue: string
	}
>(
	'@@admin',
	async (data, { extra: { client, api }, rejectWithValue }) => {
		try {
			const response = await client.post(api.URL_CREATE_COFFEE, data)
			return await response.data
		} catch (e) {
			if (e instanceof Error)
				return rejectWithValue(e.message)
			
			return rejectWithValue('Unknown error')
		}
	}, {
		condition: (_, { getState }) => {
			const { adminPanel: { status } } = getState()
			if (status === 'loading') return false
		}
	}
)