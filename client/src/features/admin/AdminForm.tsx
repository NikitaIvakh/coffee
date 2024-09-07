import { ErrorMessage, Field, Formik, useField } from 'formik'
import { useState } from 'react'
import Swal from 'sweetalert2'
import * as Yup from 'yup'
import { CoffeeItem, CoffeeType, MyFormValues, MyTextInputProps } from '../../types'
import useAdmin from './use-admin'
import "./styles/adminlForm.scss"

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

const prepareFormData = (values: MyFormValues): FormData => {
	const formData = new FormData()
	if (values?.id) {
		formData.append('id', values?.id)
	}
	formData.append('name', values.name)
	formData.append('description', values.description)
	formData.append('price', values.price.toFixed(2))
	formData.append('coffeeType', values.coffeeType.toString())
	if (values.avatar) {
		formData.append('avatar', values.avatar)
	}
	return formData
}

const showSuccessMessage = () => {
	Swal.fire({
		title: 'Great',
		text: 'The data has been successfully sent to the server!',
		icon: 'success'
	}).then()
}

interface AdminFormProps {
	title: string
	coffee?: CoffeeItem | null
}

const AdminForm = (props: AdminFormProps) => {
	const { title, coffee } = props
	const [handleCreateCoffee, handleUpdateCoffee] = useAdmin()
	const [selectedFile, setSelectedFile] = useState<File | null>(null)
	
	const initialValues: MyFormValues = coffee
		? {
			id: coffee.id,
			name: coffee.name,
			description: coffee.description,
			price: coffee.price,
			coffeeType: coffee.coffeeType.toString(),
			avatar: null
		}
		: { name: '', description: '', price: 0, coffeeType: '', avatar: null }
	
	return (
		<Formik
			initialValues={initialValues}
			validationSchema={Yup.object({
				name: Yup.string()
					.min(5, 'The length of the string must exceed 5 characters!')
					.max(1000, 'The length of the string must not exceed 1000 characters!')
					.required('This field is required!'),
				
				description: Yup.string()
					.min(5, 'The length of the string must exceed 5 characters!')
					.max(1000, 'The length of the string must not exceed 1000 characters!')
					.required('This field is required!'),
				
				price: Yup.number()
					.min(1, 'The cost cannot be less than 1')
					.max(1000, 'The cost cannot be more than 1000!')
					.required('This field is required!'),
				
				coffeeType: Yup.string().required('Поле обязательно для заполнения')
			})}
			onSubmit={(values, { resetForm }) => {
				const formData = prepareFormData(values)
				if (coffee) {
					handleUpdateCoffee(values.id!, formData)
					resetForm()
					setSelectedFile(null)
					showSuccessMessage()
				} else {
					handleCreateCoffee(formData)
					resetForm()
					setSelectedFile(null)
					showSuccessMessage()
				}
			}}
		>
			{({ handleSubmit, setFieldValue, isSubmitting }) => (
				<form className='form' onSubmit={handleSubmit}>
					<h2>{title}</h2>
					<MyTextInput label='Coffee name' id='name' name='name' type='text' />
					<MyTextInput label='Coffee description' id='description' name='description' type='text' />
					<MyTextInput label='Coffee price' id='price' name='price' type='number' />
					
					<label htmlFor='coffeeType'>Coffee type</label>
					<Field id='coffeeType' name='coffeeType' as='select'>
						<option value=''>Select the coffee type</option>
						<option value={CoffeeType.Brazil}>{CoffeeType.Brazil}</option>
						<option value={CoffeeType.Kenya}>{CoffeeType.Kenya}</option>
						<option value={CoffeeType.Columbia}>{CoffeeType.Columbia}</option>
					</Field>
					<ErrorMessage className='error' name='coffeeType' component='div' />
					
					<div className='file-input-wrapper'>
						<input
							type='file'
							id='avatar'
							name='avatar'
							className='file-input'
							onChange={(event) => {
								if (event.currentTarget.files) {
									const file = event.currentTarget.files[0]
									setSelectedFile(file)
									setFieldValue('avatar', file).then()
								}
							}}
						/>
						<label htmlFor='avatar' className='custom-file-label custom-file-label__filter'>
							{selectedFile ? selectedFile.name : 'Upload coffee picture'}
						</label>
						{selectedFile && (
							<button
								type='button'
								className='remove-file-button'
								onClick={() => {
									setSelectedFile(null)
									setFieldValue('avatar', null).then()
								}}
							>
								Remove
							</button>
						)}
					</div>
					
					<button type='submit' disabled={isSubmitting} className="form_btn form_btn__filter">{title}</button>
				</form>
			)}
		</Formik>
	)
}

export default AdminForm
