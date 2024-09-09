import { Formik, useField } from 'formik'
import { MtAuthFormValues, type MyTextInputProps } from '../../types'
import * as Yup from 'yup'
import './authForm.scss'
import { showSuccessMessage } from '../../utils'

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

interface AuthModalProps {
	title: string
}

const AuthModal = (props: AuthModalProps) => {
	const { title } = props
	
	const initialValues: MtAuthFormValues = { emailAddress: '', password: '' }
	const validationSchema = Yup.object().shape({
		emailAddress: Yup.string()
			.min(5, 'The length of the string must exceed 5 characters!')
			.max(100, 'The length of the string must not exceed 100 characters!')
			.email('Invalid email address')
			.required('This field is required!'),
		
		password: Yup.string()
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
				<h2>{title}</h2>
				<MyTextInput label='Email address' name='emailAddress' id='emailAddress' type='text' />
				<MyTextInput label='Password' id='password' name='password' type='password' />
				<button type='submit' disabled={isSubmitting} className='form_btn form_btn__filter'>{title}</button>
			</form>
		)}
		</Formik>
	)
}

export default AuthModal