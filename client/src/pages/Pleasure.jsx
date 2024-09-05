import AboutBeans from '../components/AboutBeans/AboutBeans'
import Footer from '../components/Footer/Footer'
import HeaderPromoSecond from '../components/HeaderPromo_Second/HeaderPromoSecond'
import Controls from '../features/controls/Controls'
import HeaderBgPleasure from '../resources/img/bg/last_bg.jpg'
import PleasureBg from '../resources/img/bg/goods.jpg'

const Pleasure = () => {
	return (
		<>
			<HeaderPromoSecond title='For your pleasure' backgroundImage={HeaderBgPleasure} />
			<AboutBeans title='About our goods' backgroundImage={PleasureBg} />
			<Controls path='Pleasure' />
			<Footer />
		</>
	)
}

export default Pleasure