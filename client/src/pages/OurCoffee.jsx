import AboutBeans from '../components/AboutBeans/AboutBeans'
import Controls from '../features/controls/Controls'
import Footer from '../components/Footer/Footer'
import HeaderPromoSecond from '../components/HeaderPromo_Second/HeaderPromoSecond'
import HeaderBgOurCoffee from '../resources/img/bg/second_bg.jpeg'
import AboutBeansOurCoffee from '../resources/img/bg/third_bg.jpeg'

const OurCoffee = () => {
	return (
		<>
			<HeaderPromoSecond title='Our Coffee' backgroundImage={HeaderBgOurCoffee} />
			<AboutBeans title='About our beans' backgroundImage={AboutBeansOurCoffee} />
			<Controls path="OurCoffee"/>
			<Footer />
		</>
	)
}

export default OurCoffee