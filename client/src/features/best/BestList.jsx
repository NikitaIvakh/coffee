import { useMemo } from 'react'
import SetContentList from '../../utils/SetContentList'
import { useBase } from './use-base'
import './bestList.scss'

const BestList = () => {
	const [coffees, { status }] = useBase()
	
	const renderItems = (coffees) => {
		const coffeeItems = coffees.map((coffee, i) => {
			return (
				<div key={i} className='our-best__list-item'>
					<img className='our-best__image' src={coffee.imageUrl} alt={coffee.name} />
					<div className='our-best__title'>{coffee.name}</div>
					<div className='our-best__price'>{coffee.price.toFixed(2)}$</div>
				</div>
			)
		})
		
		return (<div className='our-best__wrapper'>
			{coffeeItems}
		</div>)
	}
	
	const elements = useMemo(() => {
		return SetContentList(() => renderItems(coffees), status, coffees)
	}, [status, coffees])
	
	return (
		<section className='our-best'>
			<h2 className='our-best__header'>Our best</h2>
			{elements}
		</section>
	)
}

export default BestList
