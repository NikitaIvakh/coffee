import { useAppDispatch } from '../../store/store'
import { createNewCoffee, updateCoffee } from './admin-action'
import { clearForm } from './admin-slice'

type onSubmitCreate = (data: FormData) => void
type onSubmitUpdate = (id: string, data: FormData) => void

const useAdmin = (): [onSubmitCreate, onSubmitUpdate] => {
	const dispatch = useAppDispatch()
	
	const handleCreateCoffee: onSubmitCreate = (data) => {
		dispatch(createNewCoffee(data))
		dispatch(clearForm())
	}
	
	const handleUpdateCoffee: onSubmitUpdate = (id, data) => {
		dispatch(updateCoffee({ id, data }))
		dispatch(clearForm())
	}
	
	return [handleCreateCoffee, handleUpdateCoffee]
}

export default useAdmin
