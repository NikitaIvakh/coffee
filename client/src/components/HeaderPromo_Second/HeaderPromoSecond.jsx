import CoffeePromo from '../CoffeePromo/CoffeePromo'
import Header from '../Header/Header'
import './headerPromoSecond.scss'

const HeaderPromoSecond = () => {
	return (
		<div className='headerPromoSecond'>
			<Header />
			<CoffeePromo />
		</div>
	)
}

export default HeaderPromoSecond