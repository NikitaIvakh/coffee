import React, { useTransition } from 'react'
import { useSelector } from 'react-redux'
import { useAppDispatch } from '../../store/store'
import { selectSearch } from './controls-selectors'
import { setSearch } from './controls-slice'

type onSearch = React.ChangeEventHandler<HTMLInputElement>

const UseSearch = (): [string, boolean, onSearch] => {
	const dispatch = useAppDispatch()
	const search = useSelector(selectSearch)
	const [isPending, startTransition] = useTransition()
	
	const handleClick: onSearch = (event) => {
		startTransition(() => {
			dispatch(setSearch(event.target.value))
		})
	}
	
	return [search, isPending, handleClick]
}

export default UseSearch