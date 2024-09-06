import { useTransition } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { selectSearch } from './controls-selectors'
import { setSearch } from './controls-slice'

const UseSearch = () => {
	const dispatch = useDispatch()
	const search = useSelector(selectSearch)
	const [isPending, startTransition] = useTransition()
	
	const handleClick = event => {
		startTransition(() => {
		dispatch(setSearch(event.target.value))
		})
	}
	
	return [search, isPending, handleClick]
}

export default UseSearch