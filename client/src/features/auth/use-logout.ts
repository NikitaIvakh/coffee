import { useSelector } from 'react-redux'
import { useNavigate } from 'react-router-dom'
import { useLogoutMutation } from '../../service/authApi.ts'
import { useAppDispatch } from '../../store/store.ts'
import { selectAuthUser } from './auth-Selectors.ts'
import { setLogout } from './auth-slice.ts'

type onLogout = [string | null, () => Promise<void>]

const useLogout = (): onLogout => {
	const dispatch = useAppDispatch()
	const [logout] = useLogoutMutation()
	const navigate = useNavigate()
	const { id, userName } = useSelector(selectAuthUser)
	
	const handleLogout = async () => {
		dispatch(setLogout())
		if (id) {
			await logout(id).unwrap()
		}
		navigate('/')
	}
	
	return [userName, handleLogout]
}

export default useLogout