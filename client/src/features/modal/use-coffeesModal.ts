import { useSelector } from 'react-redux'
import { useAppDispatch } from 'store/store'
import { coffeeModalSelector } from './modal-selectors'
import { coffeeOpenModal, coffeeCloseModal } from './modal-slice'

type onClick = [boolean, () => void, () => void]

const useCoffeesModal = (): onClick => {
	const dispatch = useAppDispatch()
	const coffeeIsOpen = useSelector(coffeeModalSelector)
	
	const coffeeOpenModalWindow = () => {
		dispatch(coffeeOpenModal())
	}
	
	const coffeeCloseModalWindow = () => {
		dispatch(coffeeCloseModal())
	}
	
	return [coffeeIsOpen, coffeeOpenModalWindow, coffeeCloseModalWindow]
}

export default useCoffeesModal