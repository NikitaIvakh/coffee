import { Dispatch, SetStateAction, useEffect, useState } from 'react'
import { useNavigate } from 'react-router-dom'
import { useLoginMutation, useRegisterMutation } from '../../service/authApi.ts'
import { useAppDispatch } from '../../store/store.ts'
import { MtAuthFormValues, MyRegisterValues } from '../../types'
import useAuthModal from '../modal/use-authModal.ts'
import { setUser } from './auth-slice.ts'

type onSubmit = [boolean, Dispatch<SetStateAction<boolean>>, (value: MyRegisterValues) => Promise<void>, (value: MtAuthFormValues) => Promise<void>]

const useAuth = (): onSubmit => {
	const dispatch = useAppDispatch()
	const navigate = useNavigate()
	const [showRegister, setShowRegister] = useState(false)
	const [, , authCloseModalWindow] = useAuthModal()
	
	const [login, { data: loginData, isSuccess: isLoginSuccess }] = useLoginMutation()
	const [register] = useRegisterMutation()
	
	useEffect(() => {
		if (isLoginSuccess && loginData) {
			dispatch(setUser({
				id: loginData.value.id,
				userName: loginData.value.userName,
				jwtToken: loginData.value.jwtToken,
				refreshToken: loginData.value.refreshToken
			}))
			navigate('/')
			authCloseModalWindow()
		}
	}, [isLoginSuccess, loginData, dispatch, navigate])
	
	const handleRegister = async (values: MyRegisterValues): Promise<void> => {
		await register({
			firstName: values.firstName,
			lastName: values.lastName,
			userName: values.userName,
			emailAddress: values.emailAddress,
			password: values.password,
			passwordConform: values.passwordConform
		}).unwrap()
	}
	
	const handleLogin = async (values: MtAuthFormValues): Promise<void> => {
		await login({
			emailAddress: values.emailAddress,
			password: values.password
		}).unwrap()
	}
	
	return [showRegister, setShowRegister, handleRegister, handleLogin]
}

export default useAuth
