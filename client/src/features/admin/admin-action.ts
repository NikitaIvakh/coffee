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
			const user = JSON.parse(localStorage.getItem('user') as '{}')
			if (!user) return rejectWithValue('User data is missing')
			const response = await client.post(api.URL_CREATE_COFFEE, data, {
				headers: {
					'Authorization': `Bearer ${user.jwtToken}`
				}
			})
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
			const user = JSON.parse(localStorage.getItem('user') as '{}')
			if (!user) return rejectWithValue('User data is missing')
			const response = await client.patch(api.URL_UPDATE_COFFEE(id), data, {
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
			const user = JSON.parse(localStorage.getItem('user') as '{}')
			if (!user) return rejectWithValue('User data is missing')
			const response = await client.delete(api.URL_DELETE_COFFEE(id), {
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
			const { adminPanel: { status } } = getState()
			if (status === 'loading') return false
		}
	}
)