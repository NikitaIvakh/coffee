import { combineReducers, configureStore } from '@reduxjs/toolkit'
import axios from 'axios'
import * as api from '../config'
import { bestReducer } from '../features/best/best-slice'

const rootReducers = combineReducers({
	best: bestReducer
})

const store = configureStore({
	reducer: rootReducers,
	devTools: process.env.NODE_ENV !== 'production',
	middleware: getDefaultMiddleware => getDefaultMiddleware({
		thunk: {
			extraArgument: {
				client: axios,
				api: api
			}
		}
	})
})

export default store
