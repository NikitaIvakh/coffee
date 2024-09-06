import Details from './Details'
import Footer from '../../components/Footer/Footer'
import HeaderPromoSecond from '../../components/HeaderPromo_Second/HeaderPromoSecond'

interface ControlDetailsProps {
	title: string
	path: string
	backgroundImage: string
}

const ControlDetails = (props: ControlDetailsProps) => {
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