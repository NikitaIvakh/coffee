import { useEffect, useTransition } from 'react'
import { useSelector } from 'react-redux'
import { useAppDispatch } from '../../store/store'
import { CoffeeItem } from '../../types'
import { createPages } from '../../utils/createPages'
import { selectControls } from '../controls/controls-selectors'
import { LoadCoffees } from './coffees-action'
import { selectCoffeeInfo, selectCurrentPage, selectPageSize, selectTotalCount, selectVisibleCoffees } from './coffees-selectors'
import { setCurrentPage } from './coffees-slice'

type onSelect = (page: number) => void

const useCoffees = (): [CoffeeItem[], number[], number, boolean, ReturnType<typeof selectCoffeeInfo>, onSelect
] => {
	const dispatch = useAppDispatch()
	const controls = useSelector(selectControls)
	const { search, filter } = controls
	
	const currentPage = useSelector(selectCurrentPage)
	const pageSize = useSelector(selectPageSize)
	const totalCount = useSelector(selectTotalCount)
	
	const coffees = useSelector(state => selectVisibleCoffees(state, search, filter))
	const { status, error, qty } = useSelector(selectCoffeeInfo)
	const [isPending, startTransition] = useTransition()
	
	useEffect(() => {
		startTransition(() => {
			dispatch(setCurrentPage(1))
			dispatch(LoadCoffees({ search, filter, page: 1, pageSize }))
		})
	}, [search, filter, pageSize, dispatch])
	
	useEffect(() => {
		startTransition(() => {
			dispatch(LoadCoffees({ search, filter, page: currentPage, pageSize }))
		})
	}, [currentPage, search, filter, pageSize, dispatch])
	
	const handleClick: onSelect = page => {
		startTransition(() => {
			dispatch(setCurrentPage(page))
		})
	}
	
	const pages = createPages(totalCount, pageSize, currentPage)
	return [coffees, pages, currentPage, isPending, { status, error, qty }, handleClick]
}

export default useCoffees
