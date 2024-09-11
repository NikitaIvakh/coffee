import { useEffect } from 'react'
import { useSelector } from 'react-redux'
import Swal from 'sweetalert2'
import { useAppDispatch } from 'store/store'
import { successCreate, successDelete, successUpdate } from 'utils'
import { LoadCoffees } from '../coffees/coffees-action'
import { selectCurrentPage, selectPageSize } from '../coffees/coffees-selectors'
import { selectControls } from '../controls/controls-selectors'
import useAdminModal from '../modal/use-adminModal'
import useCoffeesModal from '../modal/use-coffeesModal'
import { createNewCoffee, deleteCoffee, updateCoffee } from './admin-action'
import { selectStatus } from './admin-selectors'
import { clearForm } from './admin-slice'

type onSubmitCreate = (data: FormData) => void
type onSubmitUpdate = (id: string, data: FormData) => void
type onSubmitDelete = (id: string) => void

const useAdmin = (): [onSubmitCreate, onSubmitUpdate, onSubmitDelete] => {
	const dispatch = useAppDispatch()
	const controls = useSelector(selectControls)
	const { search, filter } = controls
	
	const currentPage = useSelector(selectCurrentPage)
	const pageSize = useSelector(selectPageSize)
	const adminStatus = useSelector(selectStatus)
	
	const [, , adminCloseModalWindow] = useAdminModal()
	const [, , coffeeCloseModalWindow] = useCoffeesModal()
	
	const onCloseModalWindows = () => {
		adminCloseModalWindow()
		coffeeCloseModalWindow()
	}
	
	const handleCreateCoffee: onSubmitCreate = (data) => {
		successCreate().then((result) => {
			if (result.isConfirmed) {
				dispatch(createNewCoffee(data))
				Swal.fire('Saved!', 'Your data has been saved.', 'success').then(() => {
					dispatch(clearForm())
				})
			} else if (result.isDenied) {
				Swal.fire('New data are not saved', '', 'info').then(() => {
					dispatch(clearForm())
				})
			}
			
			dispatch(clearForm())
			onCloseModalWindows()
		})
	}
	
	const handleUpdateCoffee: onSubmitUpdate = (id, data) => {
		successUpdate().then((result) => {
			if (result.isConfirmed) {
				dispatch(updateCoffee({ id, data }))
				Swal.fire('Saved!', 'Your data has been updated and saved.', 'success').then(() => {
					dispatch(clearForm())
				})
			} else if (result.isDenied) {
				Swal.fire('Changes are not saved', '', 'info').then(() => {
					dispatch(clearForm())
				})
			}
			
			dispatch(clearForm())
			onCloseModalWindows()
		})
	}
	
	const handleDeleteCoffee: onSubmitDelete = (id) => {
		successDelete().then((result) => {
			if (result.isConfirmed) {
				dispatch(deleteCoffee(id))
				Swal.fire({ title: 'Deleted!', text: 'Your file has been deleted.', icon: 'success' }).then(() => {
					dispatch(clearForm())
				})
			} else if (result.isDenied) {
				Swal.fire('Changes are not saved', '', 'info').then(() => {
					dispatch(clearForm())
				})
			}
		})
		
		dispatch(clearForm())
		onCloseModalWindows()
	}
	
	useEffect(() => {
		if (adminStatus === 'confirmed') {
			dispatch(LoadCoffees({ search, filter, page: currentPage, pageSize }))
		}
	}, [adminStatus, search, filter, currentPage, pageSize, dispatch])
	
	return [handleCreateCoffee, handleUpdateCoffee, handleDeleteCoffee]
}

export default useAdmin
