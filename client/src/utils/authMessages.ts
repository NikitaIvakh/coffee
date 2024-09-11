import Swal from 'sweetalert2'

export const successLogout = () => {
	return Swal.fire({
		title: 'Are you sure?',
		text: 'You won\'t be able to revert this!',
		icon: 'warning',
		showCancelButton: true,
		confirmButtonColor: '#3085d6',
		cancelButtonColor: '#d33',
		confirmButtonText: 'Yes, logout!'
	})
}
export const successRegister = () => {
	Swal.fire({
		title: 'Successful registration!',
		text: 'You have successfully registered!',
		icon: 'success'
	})
}

export const successLogin = () => {
	Swal.fire({
		title: 'Successful login to your account!',
		text: 'You have successfully logged into your account!',
		icon: 'success'
	})
}

export const loginWithErrors = (errorMessage: string) => {
	Swal.fire({
		icon: 'error',
		title: 'Oops...',
		text: errorMessage || 'Something went wrong!'
	})
}

export const registerWithErrors = (errorMessage: string) => {
	Swal.fire({
		icon: 'error',
		title: 'Oops...',
		text: errorMessage || 'Something went wrong!'
	})
}