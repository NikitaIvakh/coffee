import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'
import { AuthRegisterValues, AuthRequestValues } from '../../types/authForm.ts'

export const authApi = createApi({
	reducerPath: 'authApi',
	baseQuery: fetchBaseQuery({baseUrl: "https://localhost:5001/api/identity"}),
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
	})
})

export const {
	useLoginMutation,
	useLogoutMutation,
	useRegisterMutation,
	useLazyConfirmEmailQuery,
} = authApi