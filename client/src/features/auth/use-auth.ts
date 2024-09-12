import { Dispatch, SetStateAction, useEffect, useState } from 'react'
import { useSelector } from 'react-redux'
import { useNavigate } from 'react-router-dom'
import { useAppDispatch } from '../../store/store.ts'
import { AuthErrors, RegisterErrors } from '../../types'
import { AuthRegisterValues, type AuthRequestValues } from '../../types/authForm.ts'
import { loginWithErrors, registerWithErrors, successLogin, successRegister } from '../../utils'
import useAuthModal from '../modal/use-authModal.ts'
import { useLoginMutation, useRegisterMutation } from './auth-apiSlice.ts'
import { selectUserAuthenticated } from './auth-Selectors.ts'
import { setUser } from './auth-slice.ts'

type onSubmit = [boolean, Dispatch<SetStateAction<boolean>>, (value: AuthRegisterValues) => Promise<void>, (value: AuthRequestValues) => Promise<void>, boolean]

const useAuth = (): onSubmit => {
	const dispatch = useAppDispatch()
	const navigate = useNavigate()
	const [showRegister, setShowRegister] = useState(false)
	const [, , authCloseModalWindow] = useAuthModal()
	const isAuthUser = useSelector(selectUserAuthenticated)
	
	const [login, { data: loginData, isSuccess: isLoginSuccess }] = useLoginMutation()
	const [register] = useRegisterMutation()
	
	useEffect(() => {
		if (isLoginSuccess && loginData) {
			dispatch(setUser(loginData.value))
			navigate('/')
			authCloseModalWindow()
		}
	}, [isLoginSuccess, loginData, dispatch, navigate])
	
	const handleRegister = async (values: AuthRegisterValues): Promise<void> => {
		try {
			await register(values).unwrap()
			successRegister()
		} catch (e: unknown) {
			const error = e as RegisterErrors
			if (error as RegisterErrors)
				registerWithErrors(error?.data?.type)
		}
	}
	
	const handleLogin = async (values: AuthRequestValues): Promise<void> => {
		try {
			await login(values).unwrap()
			successLogin()
		} catch (e: unknown) {
			const error = e as AuthErrors
			if (error as AuthErrors)
				loginWithErrors(error?.data?.type)
		}
	}
	
	
	return [showRegister, setShowRegister, handleRegister, handleLogin, isAuthUser]
}

export default useAuth
