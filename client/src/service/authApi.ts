import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'

export const authApi = createApi({
	reducerPath: 'authApi',
	baseQuery: fetchBaseQuery({
		baseUrl: 'https://localhost:9020/api/identity'
	}),
	endpoints: builder => ({
		login: builder.mutation({
			query: (body: { emailAddress: string, password: string }) => {
				return {
					url: '/Login',
					method: 'POST',
					body: body
				}
			}
		}),
		
		logout: builder.mutation({
			query: (id: string) => {
				return {
					url: `/Logout/${id}`,
					method: "DELETE",
					body: id
				}
			}
		}),
		
		register: builder.mutation({
			query: (body: {firstName: string, lastName: string, userName: string, emailAddress: string, password: string, passwordConform: string }) => {
				return {
					url: '/Register',
					method: 'POST',
					body: body
				}
			}
		})
	})
});

export const { useLoginMutation, useLogoutMutation, useRegisterMutation } = authApi