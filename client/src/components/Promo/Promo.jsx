import coffeeBeansLight from '../../resources/img/icons/coffee_beans_light.png'
import './promo.scss'

const Promo = () => {
	return (
		<section className='promo'>
			<h1 className='promo__header'>Everything You Love About Coffee</h1>
			<div className='divider-wrapper'>
				<div className='divider divider--first'></div>
				<img
					className='promo__image'
					src={coffeeBeansLight}
					alt='coffee beans light'
				/>
				<div className='divider divider--second'></div>
			</div>
			<h2 className='promo__subheader'>
				We make every day full of energy and taste
			</h2>
			<h3 className='promo__title'>Want to try our beans?</h3>
			<button className='promo__btn'>More</button>
		</section>
	)
}

export default Promo
