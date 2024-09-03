import About from '../components/About/About'
import Footer from '../components/Footer/Footer'
import HeaderPromoMain from '../components/HeaderPromo_Main/HeaderPromoMain'
import BestList from '../features/best/BestList'

const Main = () => {
	return (
		(
			<>
				<HeaderPromoMain />
				<About />
				<BestList />
				<Footer />
			</>
		)
	)
}

export default Main