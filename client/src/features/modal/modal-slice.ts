import { createSlice } from '@reduxjs/toolkit'

export type ModalSliceType = {
	coffeesIsOpen: boolean
	adminIsOpen: boolean
	authIsOpen: boolean
	registerIsOpen: boolean
}

const initialState: ModalSliceType = {
	coffeesIsOpen: false,
	adminIsOpen: false,
	authIsOpen: false,
	registerIsOpen: false
}

const ModalSlice = createSlice({
	name: '@@modal',
	initialState,
	reducers: {
		coffeeOpenModal: (state) => {
			state.coffeesIsOpen = true
		},
		coffeeCloseModal: (state) => {
			state.coffeesIsOpen = false
		},
		adminOpenModal: (state) => {
			state.adminIsOpen = true
		},
		adminCloseModal: (state) => {
			state.adminIsOpen = false
		},
		authOpenModal: (state) => {
			state.authIsOpen = true
		},
		authCloseModal: (state) => {
			state.authIsOpen = false
		},
		registerOpenModal: (state) => {
			state.registerIsOpen = true
		},
		registerCloseModal: (state) => {
			state.registerIsOpen = false
		}
	}
})

export const modal = ModalSlice.reducer
export const {
	coffeeOpenModal,
	coffeeCloseModal,
	adminOpenModal,
	adminCloseModal,
	authOpenModal,
	authCloseModal,
	registerOpenModal,
	registerCloseModal
} = ModalSlice.actions