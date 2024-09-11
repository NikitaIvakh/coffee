import { useEffect } from 'react'
import { useSelector } from 'react-redux'
import { useAppDispatch } from 'store/store'
import { CoffeeItem } from 'types'
import useAuth from '../auth/use-auth.ts'
import { loadItems } from './best-actions'
import { selectAllCoffees, selectBestInfo } from './best-selectors'
import { clearItems } from './best-slice.ts'

export const useBase = (): [CoffeeItem[], boolean, ReturnType<typeof selectBestInfo>] => {
	const dispatch = useAppDispatch()
	const coffees = useSelector(selectAllCoffees)
	const [, , , , isAuthUser] = useAuth()
	const { status, error, qty } = useSelector(selectBestInfo)
	const user = JSON.parse(localStorage.getItem('user') as "{}")
	
	useEffect(() => {
		if (isAuthUser) {
			if (!qty) {
				dispatch(loadItems());
			}
		} else {
			dispatch(clearItems());
		}
	}, [qty, dispatch, isAuthUser, user]);
	
	return [coffees, isAuthUser, { status, error, qty }]
}
