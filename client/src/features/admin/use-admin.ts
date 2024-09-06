import { useAppDispatch } from '../../store/store'
import { createNewCoffee } from './admin-action'
import { clearForm } from './admin-slice'

type onSubmit = (data: FormData) => void

const useAdmin = (): onSubmit => {
	const dispatch = useAppDispatch()
	
	return (data) => {
		dispatch(createNewCoffee(data))
		clearForm()
	}
}

export default useAdmin