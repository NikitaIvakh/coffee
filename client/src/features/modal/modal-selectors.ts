import type { RootState } from 'store/store'

export const coffeeModalSelector = (state: RootState) => state.modal.coffeesIsOpen
export const adminModalSelector = (state: RootState) => state.modal.adminIsOpen
export const authModalSelector = (state: RootState) => state.modal.authIsOpen