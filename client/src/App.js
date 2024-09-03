import About from './components/About/About'
import Header from './components/header/Header'
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
		</>
	)
}

export default App
