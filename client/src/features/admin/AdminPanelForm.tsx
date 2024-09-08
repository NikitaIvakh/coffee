import ModalWindow from '../modal/ModalWindow'
import useAdminModal from '../modal/use-adminModal'
import './styles/adminPanelForm.scss'

const AdminPanelForm = () => {
	const [adminIsOpen, adminOpenModalWindow, adminCloseModalWindow] = useAdminModal()
	
	return (
		<div className='admin'>
			<button onClick={adminOpenModalWindow} className='admin_btn admin_btn__filter'>Add Coffee</button>
			{adminIsOpen && (
				<ModalWindow
					title='Add Coffee'
					isVisible={adminIsOpen}
					onClose={adminCloseModalWindow}
				/>
			)}
		</div>
	)
}

export default AdminPanelForm
