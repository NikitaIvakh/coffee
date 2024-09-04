import { useDispatch, useSelector } from 'react-redux'
import { selectSearch } from './coffees-selectors'
import { setSearch } from './coffees-slice'

const UseSearch = () => {
	const dispatch = useDispatch()
	const search = useSelector(selectSearch)
	
	const handleClick = event => {
		dispatch(setSearch(event.target.value))
	}
	
	return [search, handleClick]
}

export default UseSearch