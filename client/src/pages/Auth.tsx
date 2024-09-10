import { useState, useEffect, ChangeEvent, FormEvent } from 'react'
import { useNavigate } from 'react-router-dom'
import { setUser } from '../features/auth/auth-slice'
import { useLoginMutation, useRegisterMutation } from '../service/authApi'
import './authForm.scss'
import { useAppDispatch } from '../store/store'
import type { MyRegisterValues } from '../types'

const Auth = () => {
	const [showRegister, setShowRegister] = useState(false)
	const dispatch = useAppDispatch()
	const navigate = useNavigate()
	const [formValues, setFormValues] = useState<MyRegisterValues>({
		firstName: '',
		lastName: '',
		userName: '',
		emailAddress: '',
		password: '',
		passwordConform: ''
	})
	
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
			navigate('/Main')
		}
	}, [isLoginSuccess, loginData, dispatch, navigate])
	
	const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
		const { name, value } = e.target
		setFormValues(prev => ({ ...prev, [name]: value }))
	}
	
	const handleSubmit = async (e: FormEvent) => {
		e.preventDefault()
		try {
			if (showRegister) {
				await register({
					firstName: formValues.firstName,
					lastName: formValues.lastName,
					userName: formValues.userName,
					emailAddress: formValues.emailAddress,
					password: formValues.password,
					passwordConform: formValues.passwordConform
				}).unwrap()
			} else {
				await login({
					emailAddress: formValues.emailAddress,
					password: formValues.password
				}).unwrap()
			}
		} catch (error) {
			console.error('Failed to login:', error)
		}
	}
	
	return (
		<form className='auth-form' onSubmit={handleSubmit}>
			<h2>{!showRegister ? 'Login' : 'Register'}</h2>
			<p>{!showRegister ? 'Please enter your Email & Password' : 'Please enter User details'}</p>
			
			{showRegister && (
				<>
					<label htmlFor='firstName'>First Name</label>
					<input
						type='text'
						id='firstName'
						name='firstName'
						value={formValues.firstName}
						onChange={handleChange}
					/>
					
					<label htmlFor='lastName'>Last Name</label>
					<input
						type='text'
						id='lastName'
						name='lastName'
						value={formValues.lastName}
						onChange={handleChange}
					/>
					
					<label htmlFor='userName'>Username</label>
					<input
						type='text'
						id='userName'
						name='userName'
						value={formValues.userName}
						onChange={handleChange}
					/>
				</>
			)}
			
			<label htmlFor='emailAddress'>Email address</label>
			<input
				type='email'
				id='emailAddress'
				name='emailAddress'
				value={formValues.emailAddress}
				onChange={handleChange}
				required
			/>
			
			<label htmlFor='password'>Password</label>
			<input
				type='password'
				id='password'
				name='password'
				value={formValues.password}
				onChange={handleChange}
				required
			/>
			
			{showRegister && (
				<>
					<label htmlFor='passwordConform'>Confirm Password</label>
					<input
						type='password'
						id='passwordConform'
						name='passwordConform'
						value={formValues.passwordConform}
						onChange={handleChange}
					/>
				</>
			)}
			
			<button type='submit' className='form_btn form_btn__filter'>
				{!showRegister ? 'Login' : 'Register'}
			</button>
			
			<div>
				<h5 className='form__wrapper'>
					{!showRegister ? (
						<>
							Don't have an account?
							<p
								className='form__wrapper-dont-account'
								style={{ cursor: 'pointer' }}
								onClick={() => setShowRegister(true)}
							>
								Sign Up
							</p>
						</>
					) : (
						<>
							Already have an account?
							<p
								className='form__wrapper-has-account'
								style={{ cursor: 'pointer' }}
								onClick={() => setShowRegister(false)}
							>
								Sign In
							</p>
						</>
					)}
				</h5>
			</div>
		</form>
	)
}

export default Auth
