import { ErrorMessage, Field, Formik, useField } from 'formik'
import useAdmin from './use-admin'
import { CoffeeType, MyTextInputProps, MyFormValues } from '../../types'
import * as Yup from 'yup'
import './styles/adminPanelForm.scss'
import Swal from 'sweetalert2'

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
		title: 'Отлично',
		text: 'Данные успешно ушли на сервер!',
		icon: 'success'
	}).then()
}

const AdminPanelForm = () => {
	const createCoffee = useAdmin()
	const initialValues: MyFormValues = { name: '', description: '', price: 0, coffeeType: '', avatar: null }
	
	return (
		<div>
			<Formik
				initialValues={initialValues}
				validationSchema={Yup.object({
					name: Yup.string()
						.min(5, 'Длина строки должна превышать 5 символов!')
						.max(1000, 'Длина строки не должна превышать 1000 символов')
						.required('Поле обязательно для заполнения'),
					
					description: Yup.string()
						.min(5, 'Длина строки должна превышать 5 символов!')
						.max(1000, 'Длина строки не должна превышать 1000 символов')
						.required('Поле обязательно для заполнения'),
					
					price: Yup.number()
						.min(1, 'Стоимость не может быть меньше 1')
						.max(1000, 'Стоимость не может быть больше 1000')
						.required('Поле обязательно для заполнения'),
					
					coffeeType: Yup.string().required('Поле обязательно для заполнения')
				})}
				onSubmit={(values, { resetForm }) => {
					const formData = prepareFormData(values)
					
					createCoffee(formData)
					resetForm()
					showSuccessMessage()
				}}>
				{({ handleSubmit, setFieldValue, isSubmitting }) => (
					<form className='form' onSubmit={handleSubmit}>
						<MyTextInput label='Наименование кофе' id='name' name='name' type='text' />
						
						<MyTextInput label='Описание кофе' id='description' name='description' type='text' />
						
						<MyTextInput label='Стоимость кофе' id='price' name='price' type='number' />
						
						<label htmlFor='coffeeType'>Тип кофе</label>
						<Field id='coffeeType' name='coffeeType' as='select'>
							<option value=''>Выберите страну производства</option>
							<option value={CoffeeType.Brazil}>Бразилия</option>
							<option value={CoffeeType.Kenya}>Кения</option>
							<option value={CoffeeType.Columbia}>Колумбия</option>
						</Field>
						<ErrorMessage className='error' name='coffeeType' component='div' />
						
						<label htmlFor='avatar' className='file-label'>Картинка кофе</label>
						<input
							type='file'
							id='avatar'
							name='avatar'
							onChange={(event) => {
								if (event.currentTarget.files) {
									setFieldValue('avatar', event.currentTarget.files[0]).then()
								}
							}}
						/>
						
						<button type='submit' disabled={isSubmitting}>Добавить кофе</button>
					</form>
				)}
			</Formik>
		</div>
	)
}

export default AdminPanelForm
