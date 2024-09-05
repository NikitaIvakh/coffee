import Details from './Details'
import Footer from '../../components/Footer/Footer'
import HeaderPromoSecond from '../../components/HeaderPromo_Second/HeaderPromoSecond'

const ControlDetails = (props) => {
	const { title, path, backgroundImage } = props
	return (
		<>
			<HeaderPromoSecond title={title} backgroundImage={backgroundImage} />
			<Details path={path} />
			<Footer />
		</>
	)
}

export default ControlDetails