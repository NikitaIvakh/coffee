import { Formik, useField } from 'formik'
import * as Yup from 'yup'
import { MyRegisterValues, type MyTextInputProps } from '../types'
import "./registerForm.scss"
import { showSuccessMessage } from '../utils'

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

const Register = () => {
	const initialValues: MyRegisterValues =
		{
			firstName: '',
			lastName: '',
			username: '',
			email: '',
			password: '',
			passwordConform: ''
		}
	
	const validationSchema = Yup.object().shape({
		firstName: Yup.string()
			.min(5, 'The length of the string must exceed 5 characters!')
			.max(100, 'The length of the string must not exceed 100 characters!')
			.email('Invalid email address')
			.required('This field is required!'),
		
		lastName: Yup.string()
			.min(5, 'The length of the string must exceed 5 characters!')
			.max(100, 'The length of the string must not exceed 100 characters!')
			.email('Invalid email address')
			.required('This field is required!'),
		
		username: Yup.string()
			.min(5, 'The length of the string must exceed 5 characters!')
			.max(100, 'The length of the string must not exceed 100 characters!')
			.email('Invalid email address')
			.required('This field is required!'),
		
		email: Yup.string()
			.min(5, 'The length of the string must exceed 5 characters!')
			.max(100, 'The length of the string must not exceed 100 characters!')
			.email('Invalid email address')
			.required('This field is required!'),
		
		password: Yup.string()
			.min(5, 'The length of the string must exceed 5 characters!')
			.max(500, 'The length of the string must not exceed 500 characters!')
			.required('This field is required!'),
		
		passwordConform: Yup.string()
			.min(5, 'The length of the string must exceed 5 characters!')
			.max(500, 'The length of the string must not exceed 500 characters!')
			.required('This field is required!')
	})
	
	return (
		<Formik
			initialValues={initialValues}
			validationSchema={validationSchema}
			onSubmit={(values, { resetForm }) => {
				console.log(JSON.stringify(values, null, 2))
				showSuccessMessage()
				resetForm()
			}}
		>{({ handleSubmit, isSubmitting }) => (
			<form className='auth-form' onSubmit={handleSubmit}>
				<h2>Login</h2>
				<MyTextInput label='First Name' name='firstName' id='firstName' type='text' />
				<MyTextInput label='Last Name' id='lastName' name='lastName' type='password' />
				<MyTextInput label='Username' id='username' name='username' type='password' />
				<MyTextInput label='email' id='email' name='email' type='password' />
				<MyTextInput label='Password' id='password' name='password' type='password' />
				<MyTextInput label='password Conform' id='passwordConform' name='passwordConform' type='password' />
				<button type='submit' disabled={isSubmitting} className='form_btn form_btn__filter'>Login</button>
			</form>
		)}
		</Formik>
	)
}

export default Register