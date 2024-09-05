import { useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { LoadCoffeeDetails } from './coffee-actions'
import { selectCoffee } from './coffee-selectors'
import { clearDetails } from './coffee-slice'

const useCoffee = (id) => {
	const dispatch = useDispatch()
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