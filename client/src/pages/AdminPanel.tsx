import HeaderPromoSecond from 'components/HeaderPromo_Second/HeaderPromoSecond'
import AdminPanelForm from '../features/admin/AdminPanelForm'
import AdminHeaderBg from '../resources/img/bg/last_bg.jpg'

export const AdminPanel = () => {
	return (
		<>
			<HeaderPromoSecond title='Admin Panel' backgroundImage={AdminHeaderBg} />
			<AdminPanelForm />
		</>
	)
}
