import coffeeFirst from '../../resources/img/coffee/coffee_first.jpeg'
import coffeeSecond from '../../resources/img/coffee/coffee_second.jpeg'
import coffeeThird from '../../resources/img/coffee/coffee_third.jpeg'
import './best.scss'

const Best = () => {
	return (
		<section className='our-best'>
			<h2 className='our-best__header'>Our best</h2>
			<div className='our-best__wrapper'>
				<div className='our-best__list-item'>
					<img className='our-best__image' src={coffeeFirst} alt='coffee' />
					<div className='our-best__title'>Solimo Coffee Beans 2 kg</div>
					<div className='our-best__price'>10.73$</div>
				</div>
				<div className='our-best__list-item'>
					<img className='our-best__image' src={coffeeSecond} alt='coffee' />
					<div className='our-best__title'>Presto Coffee Beans 1 kg</div>
					<div className='our-best__price'>15.99$</div>
				</div>
				<div className='our-best__list-item'>
					<img className='our-best__image' src={coffeeThird} alt='coffee' />
					<div className='our-best__title'>AROMISTICO Coffee 1 kg</div>
					<div className='our-best__price'>6.99$</div>
				</div>
			</div>
		</section>
	)
}

export default Best