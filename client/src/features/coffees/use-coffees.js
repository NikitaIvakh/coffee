import { useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { selectControls } from '../controls/controls-selectors'
import { LoadCoffees } from './coffees-action'
import {
	selectCoffeeInfo,
	selectCurrentPage,
	selectVisibleCoffees,
	selectPageSize,
	selectTotalCount
} from './coffees-selectors'
import { setCurrentPage } from './coffees-slice'
import { createPages } from '../../utils/createPages'

const useCoffees = () => {
	const dispatch = useDispatch()
	const controls = useSelector(selectControls)
	const { search, filter } = controls
	const currentPage = useSelector(selectCurrentPage)
	const pageSize = useSelector(selectPageSize)
	const totalCount = useSelector(selectTotalCount)
	const coffees = useSelector((state) => selectVisibleCoffees(state, search, filter))
	const { status, error, qty } = useSelector(selectCoffeeInfo)
	
	useEffect(() => {
		if (!qty) {
			dispatch(LoadCoffees({ search: search, filter, page: currentPage, pageSize }))
		}
	}, [qty, dispatch, currentPage, pageSize, search, filter])
	
	const handleClick = (page) => {
		dispatch(setCurrentPage(page))
	}
	
	const pages = createPages(totalCount, pageSize, currentPage)
	return [coffees, pages, currentPage, { status, error, qty }, handleClick]
}

export default useCoffees
