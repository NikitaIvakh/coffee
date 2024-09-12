import { Dispatch, SetStateAction, useEffect, useState } from 'react'
import { useSelector } from 'react-redux'
import { useNavigate } from 'react-router-dom'
import { useAppDispatch } from '../../store/store.ts'
import { AuthErrors, RegisterErrors } from '../../types'
import { AuthRegisterValues, type AuthRequestValues, type AuthResponseValues } from '../../types/authForm.ts'
import { loginWithErrors, registerWithErrors, successLogin, successRegister } from '../../utils'
import useAuthModal from '../modal/use-authModal.ts'
import { useLazyConfirmEmailQuery, useLoginMutation, useRegisterMutation } from './auth-apiSlice.ts'
import { selectAuthUser, selectEmailConfirmed, selectUserAuthenticated } from './auth-Selectors.ts'
import { setEmailConfirmed, setUser } from './auth-slice.ts'

type onSubmit =
	[
		boolean,
		Dispatch<SetStateAction<boolean>>,
		(value: AuthRegisterValues) => Promise<void>,
		(value: AuthRequestValues) => Promise<void>,
		boolean,
		(token: string) => Promise<void>,
		AuthResponseValues,
		boolean
	]

const useAuth = (): onSubmit => {
	const dispatch = useAppDispatch()
	const navigate = useNavigate()
	const [showRegister, setShowRegister] = useState(false)
	const [, , authCloseModalWindow] = useAuthModal()
	const isAuthUser = useSelector(selectUserAuthenticated)
	const user = useSelector(selectAuthUser)
	const isEmailConfirmed = useSelector(selectEmailConfirmed)
	
	const [login, { data: loginData, isSuccess: isLoginSuccess }] = useLoginMutation()
	const [register] = useRegisterMutation()
	const [confirmEmail] = useLazyConfirmEmailQuery()
	
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
	
	const handleConfirmEmail = async (token: string) => {
		try {
			await confirmEmail(token).unwrap()
			dispatch(setEmailConfirmed())
		} catch (e) {
			if (e instanceof Error)
				console.error('Error confirming email:', e.message)
		}
	}
	
	return [showRegister, setShowRegister, handleRegister, handleLogin, isAuthUser, handleConfirmEmail, user!, isEmailConfirmed]
}

export default useAuth
