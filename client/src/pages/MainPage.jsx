import About from '../components/About/About'
import Footer from '../components/Footer/Footer'
import Header from '../components/header/Header'
import Best from '../components/Best/Best'
import Promo from '../components/Promo/Promo'

const MainPage = () => {
	return (
		(
			<>
				<section className='wrapper'>
					<Header />
					<Promo />
				</section>
				<About />
				<Best />
				<Footer />
			</>
		)
	)
}

export  default MainPage