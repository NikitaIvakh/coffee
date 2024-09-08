import { useTransition } from 'react'
import { useSelector } from 'react-redux'
import { useAppDispatch } from 'store/store'
import type { Filter } from 'types'
import { selectFilters } from './controls-selectors'
import { setFilter } from './controls-slice'

type onSelect = (filterName: string) => void

const UseFilter = (): [Filter, boolean, onSelect] => {
	const dispatch = useAppDispatch()
	const filter = useSelector(selectFilters)
	const [isPending, startTransition] = useTransition()
	
	const handleClick: onSelect = (filterName) => {
		startTransition(() => {
			dispatch(setFilter(filterName === '' ? 'All' : filterName))
		})
	}
	
	return [filter, isPending, handleClick]
}

export default UseFilter