import { combineReducers, configureStore } from '@reduxjs/toolkit'
import axios from 'axios'
import * as api from '../config'
import { bestReducer } from '../features/best/best-slice'
import { controlsReducer } from '../features/controls/controls-slice'

const rootReducers = combineReducers({
	best: bestReducer,
	controls: controlsReducer
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
