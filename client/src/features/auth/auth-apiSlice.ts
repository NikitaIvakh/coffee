import { createApi } from '@reduxjs/toolkit/query/react'
import { baseQueryWithReauth } from '../../service/baseQueryWithReauth.ts'
import { type AuthRefreshToken, AuthRegisterValues, AuthRequestValues } from '../../types/authForm.ts'

export const authApi = createApi({
	reducerPath: 'authApi',
	baseQuery: baseQueryWithReauth,
	endpoints: builder => ({
		login: builder.mutation({
			query: (body: AuthRequestValues) => ({
				url: '/Login',
				method: 'POST',
				body: { ...body }
			})
		}),
		logout: builder.mutation({
			query: (id: string) => ({
				url: `/Logout/${id}`,
				method: 'DELETE'
			})
		}),
		register: builder.mutation({
			query: (body: AuthRegisterValues) => ({
				url: '/Register',
				method: 'POST',
				body: { ...body }
			})
		}),
		confirmEmail: builder.query<void, string>({
			query: (token: string) => ({
				url: `/VerifyEmailToken/${token}`,
				method: 'GET'
			})
		}),
		refreshToken: builder.mutation({
			query: (body: AuthRefreshToken) => ({
				url: '/RefreshToken',
				method: 'PATCH',
				body: { ...body }
			})
		})
	})
})

export const {
	useLoginMutation,
	useLogoutMutation,
	useRegisterMutation,
	useLazyConfirmEmailQuery,
	useRefreshTokenMutation
} = authApi