import { createSlice } from '@reduxjs/toolkit'

export type ModalSliceType = {
	coffeesIsOpen: boolean
	adminIsOpen: boolean
}

const initialState: ModalSliceType = {
	coffeesIsOpen: false,
	adminIsOpen: false
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
		}
	}
})

export const modal = ModalSlice.reducer
export const { coffeeOpenModal, coffeeCloseModal, adminOpenModal, adminCloseModal } = ModalSlice.actions