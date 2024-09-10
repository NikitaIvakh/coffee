import { useSelector } from 'react-redux'
import { useAppDispatch } from '../../store/store.ts'
import { authModalSelector } from './modal-selectors.ts'
import { authCloseModal, authOpenModal } from './modal-slice.ts'

type useAuthModalType = [boolean, () => void, () => void]

const useAuthModal = (): useAuthModalType => {
	const dispatch = useAppDispatch()
	const authIsOpen = useSelector(authModalSelector)
	
	const authOpenModalWindow = () => {
		dispatch(authOpenModal())
	}
	
	const authCloseModalWindow = () => {
		dispatch(authCloseModal())
	}
	
	return [authIsOpen, authOpenModalWindow, authCloseModalWindow]
}

export default useAuthModal