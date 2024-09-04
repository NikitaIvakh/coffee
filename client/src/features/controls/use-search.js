import { useDispatch, useSelector } from 'react-redux'
import { selectSearch } from './controls-selectors'
import { setSearch } from './controls-slice'

const UseSearch = () => {
	const dispatch = useDispatch()
	const search = useSelector(selectSearch)
	
	const handleClick = event => {
		dispatch(setSearch(event.target.value))
	}
	
	return [search, handleClick]
}

export default UseSearch