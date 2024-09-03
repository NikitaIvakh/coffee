import About from '../components/About/About'
import Footer from '../components/Footer/Footer'
import Header from '../components/header/Header'
import Promo from '../components/Promo/Promo'
import BestList from '../features/best/BestList'

const Main = () => {
	return (
		(
			<>
				<section className='wrapper'>
					<Header />
					<Promo />
				</section>
				<About />
				<BestList />
				<Footer />
			</>
		)
	)
}

export default Main