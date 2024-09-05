import { combineReducers, configureStore } from '@reduxjs/toolkit'
import axios from 'axios'
import { thunk } from 'redux-thunk'
import * as api from '../config'
import { bestReducer } from '../features/best/best-slice'
import { coffeeList } from '../features/coffees/coffees-slice'
import { controlsReducer } from '../features/controls/controls-slice'
import { coffeeDetailsReducer } from '../features/details/coffee-slice'

const rootReducers = combineReducers({
	best: bestReducer,
	controls: controlsReducer,
	coffees: coffeeList,
	coffeeDetails: coffeeDetailsReducer
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
	}).concat(thunk)
})

export default store
