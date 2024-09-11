import Swal from 'sweetalert2'

export const successCreate = () => {
	return Swal.fire({
		title: "Do you want to save the changes?",
		showDenyButton: true,
		showCancelButton: true,
		confirmButtonText: "Save",
		denyButtonText: `Don't save`
	})
}

export const successDelete = () => {
	return Swal.fire({
		title: "Are you sure?",
		text: "You won't be able to revert this!",
		icon: "warning",
		showCancelButton: true,
		confirmButtonColor: "#3085d6",
		cancelButtonColor: "#d33",
		confirmButtonText: "Yes, delete it!"
	})
}

export const successUpdate = () => {
	return Swal.fire({
		title: "Do you want to save the changes?",
		showDenyButton: true,
		showCancelButton: true,
		confirmButtonText: "Save",
		denyButtonText: `Don't save`
	});
};