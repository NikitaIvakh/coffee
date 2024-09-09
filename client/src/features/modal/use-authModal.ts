import { useSelector } from 'react-redux'
import { useAppDispatch } from '../../store/store.ts'
import { authModalSelector } from './modal-selectors.ts'
import { authCloseModal, authOpenModal } from './modal-slice.ts'

type onModal = [boolean, () => void, () => void]

const useAuthModal = (): onModal => {
	const dispatch = useAppDispatch()
	const authIsOpen = useSelector(authModalSelector)
	
	const handleOpenAuthModal = () => {
		dispatch(authOpenModal())
	}
	
	const handleCloseAuthModal = () => {
		dispatch(authCloseModal())
	}
	
	return [authIsOpen, handleOpenAuthModal, handleCloseAuthModal]
}

export default useAuthModal