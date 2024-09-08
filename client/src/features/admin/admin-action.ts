import { createAsyncThunk } from '@reduxjs/toolkit'
import { Extra } from 'types'
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
	'@@admin/create',
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

export const updateCoffee = createAsyncThunk<
	void,
	{ id: string, data: FormData },
	{
		extra: Extra,
		state: { adminPanel: AdminSliceType },
		rejectValue: string
	}
>(
	'@@admin/update',
	async ({ id, data }, { extra: { client, api }, rejectWithValue }) => {
		try {
			const response = await client.patch(api.URL_UPDATE_COFFEE(id), data)
			return response.data
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

export const deleteCoffee = createAsyncThunk<
	void,
	string,
	{
		extra: Extra,
		state: { adminPanel: AdminSliceType },
		rejectValue: string
	}
>(
	'@@admin/delete',
	async (id, { extra: { client, api }, rejectWithValue }) => {
		try {
			const response = await client.delete(api.URL_DELETE_COFFEE(id))
			return response.data
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