import { Formik, type FormikHelpers, useField } from 'formik'
import * as Yup from 'yup'
import { MyTextInputProps } from '../../types'
import type { AuthRegisterValues, AuthRequestValues } from '../../types/authForm.ts'
import useAuth from './use-auth'
import './style/authForm.scss'

const MyTextInput = ({ label, ...props }: MyTextInputProps) => {
	const [field, meta] = useField(props)
	
	return (
		<>
			<label htmlFor={props.name}>{label}</label>
			<input {...props} {...field} />
			{meta.touched && meta.error ? (
				<div className='error'>{meta.error}</div>
			) : null}
		</>
	)
}

const Auth = () => {
	const [showRegister, setShowRegister, handleRegister, handleLogin] = useAuth()
	
	const initialValuesRegister: AuthRegisterValues = {
		firstName: '',
		lastName: '',
		userName: '',
		emailAddress: '',
		password: '',
		passwordConform: ''
	}
	
	const initialValuesLogin: AuthRequestValues = {
		emailAddress: '',
		password: ''
	}
	
	const validationSchemaRegister = Yup.object().shape({
		firstName: Yup.string()
			.min(5, 'The length of the string must exceed 5 characters!')
			.max(1000, 'The length of the string must exceed 1000 characters!')
			.required('This field is required!'),
		lastName: Yup.string()
			.min(5, 'The length of the string must exceed 5 characters!')
			.max(1000, 'The length of the string must exceed 1000 characters!')
			.required('This field is required!'),
		userName: Yup.string()
			.min(5, 'The length of the string must exceed 5 characters!')
			.max(1000, 'The length of the string must exceed 1000 characters!')
			.required('This field is required!'),
		emailAddress: Yup.string()
			.min(5, 'The length of the string must exceed 5 characters!')
			.max(1000, 'The length of the string must exceed 1000 characters!')
			.matches(
				/^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-]{2,}(?:\.[a-zA-Z0-9-]{2,})?$/,
				'Email address does not match the required format'
			)
			.email('Invalid email address')
			.required('This field is required!'),
		password: Yup.string()
			.min(5, 'The length of the string must exceed 5 characters!')
			.required('This field is required!'),
		passwordConform: Yup.string()
			.oneOf([Yup.ref('password'), null!], 'Passwords must match')
			.required('This field is required!')
	})
	
	const validationSchemaLogin = Yup.object().shape({
		emailAddress: Yup.string()
			.email('Invalid email address')
			.required('This field is required!'),
		password: Yup.string()
			.min(5, 'The length of the string must exceed 5 characters!')
			.required('This field is required!')
	})
	
	const handleSwitch = (resetForm: FormikHelpers<AuthRegisterValues | AuthRequestValues>['resetForm']) => {
		setShowRegister(!showRegister)
		
		if (showRegister)
			resetForm({ values: initialValuesLogin })
		
		else
			resetForm({ values: initialValuesRegister })
	}
	
	return (
		<Formik
			initialValues={showRegister ? initialValuesRegister : initialValuesLogin}
			validationSchema={showRegister ? validationSchemaRegister : validationSchemaLogin}
			onSubmit={async (values, { resetForm }) => {
				try {
					if (showRegister) {
						await handleRegister(values as AuthRegisterValues)
					} else {
						await handleLogin(values as AuthRequestValues)
					}
					resetForm()
				} catch (error) {
					if (error instanceof Error) {
						console.error('Error during submission', error)
					}
					resetForm()
				}
			}}
		>
			{({ handleSubmit, isSubmitting, resetForm }) => (
				<form className='auth-form' onSubmit={handleSubmit}>
					<h2>{!showRegister ? 'Login' : 'Register'}</h2>
					{showRegister ? (
						<>
							<MyTextInput label='First Name' id='firstName' name='firstName' type='text' />
							<MyTextInput label='Last Name' id='lastName' name='lastName' type='text' />
							<MyTextInput label='User Name' id='userName' name='userName' type='text' />
							<MyTextInput label='Email Address' id='emailAddress' name='emailAddress' type='text' />
							<MyTextInput label='Password' id='password' name='password' type='password' />
							<MyTextInput label='Confirm Password' id='passwordConform' name='passwordConform' type='password' />
						</>
					) : (
						<>
							<MyTextInput label='Email Address' id='emailAddress' name='emailAddress' type='text' />
							<MyTextInput label='Password' id='password' name='password' type='password' />
						</>
					)}
					<button type='submit' className='form_btn form_btn__filter' disabled={isSubmitting}>
						{!showRegister ? 'Login' : 'Register'}
					</button>
					
					<div className='form__footer'>
						<h5 className='form__footer-text'>
							{!showRegister ? (
								<>
									Don't have an account?
									<p className='form__footer-no-account'
									   onClick={() => handleSwitch(resetForm)}>
										Sign Up
									</p>
								</>
							) : (
								<>
									Already have an account?
									<p className='form__footer-has-account'
									   onClick={() => handleSwitch(resetForm)}>
										Sign In
									</p>
								</>
							)}
						</h5>
					</div>
				</form>
			)}
		</Formik>
	)
}

export default Auth
