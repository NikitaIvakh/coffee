import { useSelector } from 'react-redux'
import { useNavigate } from 'react-router-dom'
import Swal from 'sweetalert2'
import { useAppDispatch } from '../../store/store.ts'
import { AuthResponseValues } from '../../types/authForm.ts'
import { successLogout } from '../../utils'
import { useLogoutMutation } from './auth-apiSlice.ts'
import { selectAuthUser } from './auth-Selectors.ts'
import { setLogout, setUserNoAuthenticated } from './auth-slice.ts'

type onLogout = [AuthResponseValues | null, () => Promise<void>]

const useLogout = (): onLogout => {
	const dispatch = useAppDispatch()
	const [logout] = useLogoutMutation()
	const navigate = useNavigate()
	const user = useSelector(selectAuthUser)
	
	const handleLogout = async () => {
		successLogout().then((result) => {
			if (result.isConfirmed) {
				dispatch(setLogout())
				if (user?.id) {
					logout(user.id).unwrap()
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
	
	return [user, handleLogout]
}

export default useLogout