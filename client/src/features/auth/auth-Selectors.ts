import type { RootState } from '../../store/store.ts'

export const selectAuthUser = (state:RootState) => state.auth
export const selectUserAuthenticated = (state:RootState) => state.auth.isAuthenticated