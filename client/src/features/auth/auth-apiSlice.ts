import { createApi } from '@reduxjs/toolkit/query/react'
import { baseQueryWithReauth } from '../../service/baseQueryWithReauth.ts'

export const authApi = createApi({
	reducerPath: 'authApi',
	baseQuery: baseQueryWithReauth,
	endpoints: builder => ({
		login: builder.mutation({
			query: (body: { emailAddress: string, password: string }) => ({
				url: '/Login',
				method: 'POST',
				body
			})
		}),
		logout: builder.mutation({
			query: (id: string) => ({
				url: `/Logout/${id}`,
				method: 'DELETE'
			})
		}),
		register: builder.mutation({
			query: (body: { firstName: string, lastName: string, userName: string, emailAddress: string, password: string, passwordConform: string
			}) => ({
				url: '/Register',
				method: 'POST',
				body
			})
		}),
		refreshToken: builder.mutation({
			query: (body: { refreshToken: string }) => ({
				url: '/RefreshToken',
				method: 'PATCH',
				body
			})
		})
	})
})

export const { useLoginMutation, useLogoutMutation, useRegisterMutation, useRefreshTokenMutation } = authApi