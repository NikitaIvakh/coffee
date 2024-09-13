import HeaderPromoSecond from 'components/HeaderPromo_Second/HeaderPromoSecond'
import Footer from 'components/Footer/Footer'
import AdminPanelForm from 'features/admin/AdminPanelForm'
import Controls from 'features/controls/Controls'
import AdminHeaderBg from 'resources/img/bg/last_bg.jpg'

export const AdminPanel = () => {
	return (
		<>
			<HeaderPromoSecond title='Admin Panel' backgroundImage={AdminHeaderBg} />
			<AdminPanelForm />
			<Controls path='AdminPanel' showButtons={true} />
			<Footer />
		</>
	)
}
