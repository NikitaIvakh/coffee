import { useDispatch, useSelector } from 'react-redux'
import { selectFilters } from './coffees-selectors'
import { setFilter } from './coffees-slice'

const UseFilter = () => {
	const dispatch = useDispatch()
	const filter = useSelector(selectFilters)
	
	const handleClick = (filterName) => {
		dispatch(setFilter(filterName === '' ? 'All' : filterName))
	}
	
	return [filter, handleClick]
}

export default UseFilter