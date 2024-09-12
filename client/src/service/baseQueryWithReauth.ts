import { BaseQueryFn, FetchArgs, fetchBaseQuery, FetchBaseQueryError, QueryReturnValue } from '@reduxjs/toolkit/query/react'
import { setLogout, setUser } from '../features/auth/auth-slice.ts'
import type { RootState } from '../store/store.ts'

interface ApiResponse {
	value: {
		id: string,
		firstName: string,
		lastName: string,
		userName: string,
		emailAddress: string,
		role: string
		jwtToken: string,
		refreshToken: string
	}
}

type BaseQueryResult = QueryReturnValue<ApiResponse, FetchBaseQueryError, object>

const baseQuery = fetchBaseQuery({
	baseUrl: 'https://localhost:9020/api/identity',
	credentials: 'include',
	prepareHeaders: (headers, { getState }) => {
		const jwtToken = (getState() as RootState).auth.user?.jwtToken
		if (jwtToken) {
			headers.set('Authorization', `Bearer ${jwtToken}`)
		}
		
		return headers
	}
})

export const baseQueryWithReauth: BaseQueryFn<string | FetchArgs, unknown, FetchBaseQueryError> = async (args, api, extraOptions) => {
	const user = (api.getState() as RootState).auth?.user
	
	let result = await baseQuery(args, api, extraOptions) as BaseQueryResult
	if (result.error && result.error.status === 401) {
		const refreshResult = await baseQuery({
			url: '/RefreshToken',
			method: 'PATCH',
			body: {
				jwtToken: user?.jwtToken,
				refreshToken: user?.refreshToken
			}
		}, api, extraOptions) as BaseQueryResult
		
		if (refreshResult.data) {
			api.dispatch(setUser(refreshResult.data.value))
			result = await baseQuery(args, api, extraOptions) as BaseQueryResult
		} else {
			api.dispatch(setLogout())
		}
	} else if (result.error) {
		api.dispatch(setLogout())
	}
	
	return result
}