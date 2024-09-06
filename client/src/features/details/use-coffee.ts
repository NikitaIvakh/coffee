import { useEffect } from 'react'
import { useSelector } from 'react-redux'
import { useAppDispatch } from '../../store/store'
import { LoadCoffeeDetails } from './coffee-actions'
import { selectCoffee } from './coffee-selectors'
import { clearDetails, CoffeeSliceType } from './coffee-slice'

const useCoffee = (id: string): CoffeeSliceType => {
	const dispatch = useAppDispatch()
	const coffee = useSelector(selectCoffee)
	
	useEffect(() => {
		dispatch(LoadCoffeeDetails(id))
		
		return () => {
			dispatch(clearDetails())
		}
	}, [id, dispatch])
	
	return coffee
}

export default useCoffee