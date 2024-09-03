import { useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { loadItems } from './best-actions'
import { selectAllCoffees, selectBestInfo } from './best-slice'

export const useBase = () => {
	const dispatch = useDispatch()
	const coffees = useSelector(selectAllCoffees)
	const { status, error, qty } = useSelector(selectBestInfo)
	
	useEffect(() => {
		if (!qty) {
			dispatch(loadItems())
		}
	}, [qty, dispatch])
	
	return [coffees, { status, error, qty }]
}
