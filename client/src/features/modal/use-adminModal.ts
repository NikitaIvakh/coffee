import { useSelector } from 'react-redux'
import { useAppDispatch } from '../../store/store'
import { adminModalSelector } from './modal-selectors'
import { adminCloseModal, adminOpenModal } from './modal-slice'

type onClick = [boolean, () => void, () => void]


const useAdminModal = (): onClick => {
	const dispatch = useAppDispatch()
	const adminIsOpen = useSelector(adminModalSelector)
	
	const adminOpenModalWindow = () => {
		dispatch(adminOpenModal())
	}
	
	const adminCloseModalWindow = () => {
		dispatch(adminCloseModal())
	}
	
	return [adminIsOpen, adminOpenModalWindow, adminCloseModalWindow]
}

export default useAdminModal