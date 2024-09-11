import { combineReducers, configureStore } from '@reduxjs/toolkit'
import axios from 'axios'
import { useDispatch } from 'react-redux'
import * as api from '../config'
import { admin } from 'features/admin/admin-slice'
import { bestReducer } from 'features/best/best-slice'
import { coffeeList } from 'features/coffees/coffees-slice'
import { controlsReducer } from 'features/controls/controls-slice'
import { coffeeDetailsReducer } from 'features/details/coffee-slice'
import { modal } from 'features/modal/modal-slice'
import { authApi } from '../features/auth/auth-apiSlice.ts'
import { auth } from '../features/auth/auth-slice.ts'

const rootReducers = combineReducers({
	best: bestReducer,
	controls: controlsReducer,
	coffees: coffeeList,
	coffeeDetails: coffeeDetailsReducer,
	adminPanel: admin,
	modal: modal,
	[authApi.reducerPath]: authApi.reducer,
	auth: auth,
})

export const store = configureStore({
	reducer: rootReducers,
	middleware: (getDefaultMiddleware) => getDefaultMiddleware({
		thunk: {
			extraArgument: {
				client: axios,
				api: api
			}
		}
	}).concat(authApi.middleware),
	devTools: true
})

export type RootState = ReturnType<typeof rootReducers>
export type AppDispatch = typeof store.dispatch
export const useAppDispatch: () => AppDispatch = useDispatch
