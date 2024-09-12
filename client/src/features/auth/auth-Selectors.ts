import type { RootState } from '../../store/store.ts'

export const selectAuthUser = (state: RootState) => state.auth.user
export const selectUserAuthenticated = (state: RootState) => state.auth.isAuthenticated
export const selectEmailConfirmed = (state: RootState) => state.auth.isEmailConfirmed
