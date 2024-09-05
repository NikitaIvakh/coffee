import { Fragment } from 'react'
import { Link } from 'react-router-dom'
import CoffeeImg from '../../resources/img/coffee/coffee_big.jpg'
import CoffeeBeansBlack from '../../resources/img/icons/main_beans_dark.svg'

const Info = (props) => {
	const { name, coffeeType, description, price } = props
	
	return (
		<Fragment>
			<div className='details__img'>
				<img src={CoffeeImg} alt={name} />
			</div>
			<div className='details__descr'>
				<h2 className='details__title'>About it</h2>
				<div className='divider-wrapper'>
					<div className='divider divider--first'></div>
					<img src={CoffeeBeansBlack} alt='coffee beans light' />
					<div className='divider divider--second'></div>
				</div>
				<div className='details__country'><span>Country</span>: {coffeeType}</div>
				<div className='details__description'><span>Description</span>: {description}
				</div>
				<div className='details__price'><span>Price:</span> {price}$</div>
			</div>
			<Link to='/OurCoffee' className='details__home'>Go to Homepage</Link>
		</Fragment>
	)
}

export default Info