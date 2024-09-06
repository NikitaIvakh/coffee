import { useEffect, useTransition } from 'react'
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
	const [isPending, startTransition] = useTransition()
	
	// Загрузка данных с учетом фильтров и текущей страницы
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
	
	const handleClick = (page) => {
		startTransition(() => {
			dispatch(setCurrentPage(page))
		})
	}
	
	const pages = createPages(totalCount, pageSize, currentPage)
	return [coffees, pages, currentPage, isPending, { status, error, qty }, handleClick]
}

export default useCoffees
