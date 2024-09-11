import { useSelector } from 'react-redux'
import { useNavigate } from 'react-router-dom'
import Swal from 'sweetalert2'
import { useAppDispatch } from '../../store/store.ts'
import { successLogout } from '../../utils'
import { useLogoutMutation } from './auth-apiSlice.ts'
import { selectAuthUser } from './auth-Selectors.ts'
import { setLogout, setUserNoAuthenticated } from './auth-slice.ts'

type onLogout = [string | null, () => Promise<void>]

const useLogout = (): onLogout => {
	const dispatch = useAppDispatch()
	const [logout] = useLogoutMutation()
	const navigate = useNavigate()
	const { id, userName } = useSelector(selectAuthUser)
	
	const handleLogout = async () => {
		successLogout().then((result) => {
			if (result.isConfirmed) {
				dispatch(setLogout())
				if (id) {
					logout(id).unwrap()
					dispatch(setUserNoAuthenticated())
				}
				navigate('/')
				Swal.fire({
					title: 'Logout!', text: 'You have successfully logged out of your account.',
					icon: 'success'
				})
				dispatch(setLogout())
			}
		})
	}
	
	return [userName, handleLogout]
}

export default useLogout