import About from './components/About/About'
import Header from './components/header/Header'
import OurBest from './components/OurBest/OurBest'
import Promo from './components/Promo/Promo'
import './styles/app.scss'

function App() {
	return (
		<>
			<div className='wrapper'>
				<Header />
				<Promo />
			</div>
			<About />
			<OurBest />
		</>
	)
}

export default App
