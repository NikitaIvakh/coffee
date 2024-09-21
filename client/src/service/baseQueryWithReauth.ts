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

const decodeJWT = (token: string) => {
	const base64Url = token.split('.')[1]
	const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/')
	const jsonPayload = decodeURIComponent(
		atob(base64)
			.split('')
			.map(function(c) {
				return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2)
			})
			.join('')
	)
	
	return JSON.parse(jsonPayload)
}

export const isTokenExpired = (token: string) => {
	const decoded = decodeJWT(token)
	const currentTime = Math.floor(Date.now() / 1000)
	return decoded.exp < currentTime
}

type BaseQueryResult = QueryReturnValue<ApiResponse, FetchBaseQueryError, object>

const baseQuery = fetchBaseQuery({
	baseUrl: 'https://localhost:5001/api/identity',
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
	const user = (api.getState() as RootState).auth?.user;
	
	if (user?.jwtToken && isTokenExpired(user.jwtToken)) {
		console.log("Токен истек. Необходимо обновить.");
		
		const refreshResult = await baseQuery({
			url: '/RefreshToken',
			method: 'PATCH',
			body: {
				AccessToken: user?.jwtToken,
				RefreshToken: user?.refreshToken,
			},
		}, api, extraOptions) as BaseQueryResult;
		
		if (refreshResult.data) {
			api.dispatch(setUser(refreshResult.data.value));
		} else {
			api.dispatch(setLogout());
		}
	}
	
	const result = await baseQuery(args, api, extraOptions) as BaseQueryResult;
	
	if (result.error && result.error.status === 401) {
		api.dispatch(setLogout());
	}
	
	return result;
};
