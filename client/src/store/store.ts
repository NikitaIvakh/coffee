import { combineReducers, configureStore } from '@reduxjs/toolkit'
import axios from 'axios'
import { useDispatch } from 'react-redux'
import { thunk } from 'redux-thunk'
import * as api from '../config'
import { admin } from '../features/admin/admin-slice'
import { bestReducer } from '../features/best/best-slice'
import { coffeeList } from '../features/coffees/coffees-slice'
import { controlsReducer } from '../features/controls/controls-slice'
import { coffeeDetailsReducer } from '../features/details/coffee-slice'

const rootReducers = combineReducers({
	best: bestReducer,
	controls: controlsReducer,
	coffees: coffeeList,
	coffeeDetails: coffeeDetailsReducer,
	adminPanel: admin
})

export const store = configureStore({
	reducer: rootReducers,
	devTools: process.env.NODE_ENV !== 'production',
	middleware: getDefaultMiddleware => getDefaultMiddleware({
		thunk: {
			extraArgument: {
				client: axios,
				api: api
			}
		}
	}).concat(thunk)
})

export type RootState = ReturnType<typeof rootReducers>
export type AppDispatch = typeof store.dispatch
export const useAppDispatch: () => AppDispatch = useDispatch
