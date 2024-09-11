import { BaseQueryFn, FetchArgs, fetchBaseQuery, FetchBaseQueryError, QueryReturnValue } from '@reduxjs/toolkit/query/react'
import { setLogout, setUser } from '../features/auth/auth-slice.ts'
import type { RootState } from '../store/store.ts'

interface ApiResponse {
	value: {
		id: string;
		userName: string;
		role: string,
		jwtToken: string;
		refreshToken: string;
	}
}

type BaseQueryResult = QueryReturnValue<ApiResponse, FetchBaseQueryError, object>

const baseQuery = fetchBaseQuery({
	baseUrl: 'https://localhost:9020/api/identity',
	credentials: 'include',
	prepareHeaders: (headers, { getState }) => {
		const jwtToken = (getState() as RootState).auth.jwtToken
		if (jwtToken) {
			headers.set('Authorization', `Bearer ${jwtToken}`)
		}
		
		return headers
	}
})

export const baseQueryWithReauth: BaseQueryFn<string | FetchArgs, unknown, FetchBaseQueryError> = async (args, api, extraOptions) => {
	const { jwtToken, refreshToken } = (api.getState() as RootState).auth
	
	let result = await baseQuery(args, api, extraOptions) as BaseQueryResult
	if (result.error && result.error.status === 401) {
		const refreshResult = await baseQuery({
			url: '/RefreshToken',
			method: 'POST',
			body: { jwtToken, refreshToken }
		}, api, extraOptions) as BaseQueryResult
		
		if (refreshResult.data) {
			const { id, userName, role, jwtToken, refreshToken } = refreshResult.data.value
			api.dispatch(setUser({ id, userName, role, jwtToken, refreshToken }))
			result = await baseQuery(args, api, extraOptions) as BaseQueryResult
		} else {
			api.dispatch(setLogout())
		}
	} else if (result.error) {
		api.dispatch(setLogout())
	}
	
	return result
}