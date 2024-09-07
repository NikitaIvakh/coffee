import Swal from 'sweetalert2'

export const showSuccessMessage = () => {
	Swal.fire({
		title: 'Great',
		text: 'The data has been successfully sent to the server!',
		icon: 'success'
	}).then()
}

export const showDeleteMessage = () => {
	const swalWithBootstrapButtons = Swal.mixin({
		customClass: {
			confirmButton: 'btn btn-success',
			cancelButton: 'btn btn-danger'
		},
		buttonsStyling: false
	})
	
	return swalWithBootstrapButtons.fire({
		title: 'Are you sure?',
		text: 'You won\'t be able to revert this!',
		icon: 'warning',
		showCancelButton: true,
		confirmButtonText: 'Yes, delete it!',
		cancelButtonText: 'No, cancel!',
		reverseButtons: true
	})
}

