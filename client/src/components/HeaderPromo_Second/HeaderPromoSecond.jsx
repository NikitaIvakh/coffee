import CoffeePromo from '../CoffeePromo/CoffeePromo'
import Header from '../Header/Header'
import './headerPromoSecond.scss'

const HeaderPromoSecond = (props) => {
	const { title, backgroundImage } = props
	
	return (
		<div className='headerPromoSecond' style={{ background: `url(${backgroundImage}) center center/cover no-repeat` }}>
			<Header />
			<CoffeePromo title={title} />
		</div>
	)
}

export default HeaderPromoSecond