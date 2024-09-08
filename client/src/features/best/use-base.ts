import { useEffect } from 'react'
import { useSelector } from 'react-redux'
import { useAppDispatch } from 'store/store'
import { CoffeeItem } from 'types'
import { loadItems } from './best-actions'
import { selectAllCoffees, selectBestInfo } from './best-selectors'

export const useBase = (): [CoffeeItem[], ReturnType<typeof selectBestInfo>] => {
	const dispatch = useAppDispatch()
	const coffees = useSelector(selectAllCoffees)
	const { status, error, qty } = useSelector(selectBestInfo)
	
	useEffect(() => {
		if (!qty) {
			dispatch(loadItems())
		}
	}, [qty, dispatch])
	
	return [coffees, { status, error, qty }]
}
