import { useTransition } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { selectFilters } from './controls-selectors'
import { setFilter } from './controls-slice'

const UseFilter = () => {
	const dispatch = useDispatch()
	const filter = useSelector(selectFilters)
	const [isPending, startTransition] = useTransition()
	
	const handleClick = (filterName) => {
		startTransition(() => {
			dispatch(setFilter(filterName === '' ? 'All' : filterName))
		})
	}
	
	return [filter, isPending, handleClick]
}

export default UseFilter