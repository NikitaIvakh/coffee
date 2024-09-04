import { useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { selectControls } from '../controls/controls-selectors'
import { LoadCoffees } from './coffees-action'
import { selectCoffeeInfo, selectVisibleCoffees } from './coffees-selectors'

const UseCoffees = () => {
	const dispatch = useDispatch()
	const controls = useSelector(selectControls)
	const { search, filter } = controls
	const coffees = useSelector((state) => selectVisibleCoffees(state, search, filter))
	
	const { status, error, qty } = useSelector(selectCoffeeInfo)
	
	useEffect(() => {
		if (!qty)
			dispatch(LoadCoffees())
	}, [qty, dispatch])
	
	return [coffees, { status, error, qty }]
}

export default UseCoffees