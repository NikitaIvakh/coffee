import './coffeeList.scss'
import { useMemo } from 'react'
import SetContentList from '../../utils/SetContentList'

const CoffeeList = ({ process, filteredCoffees }) => {
	const renderItems = (coffees) => {
		const coffeeItems = coffees.map((coffee, i) => {
			const { imageUrl, name, coffeeType, price } = coffee
			return (
				<div className='coffees-item' key={i}>
					<img src={imageUrl} alt={name} />
					<div className='coffees-item__title'>{name}</div>
					<div className='coffees-item__sort'>{coffeeType}</div>
					<div className='coffees-item__price'>{price}$</div>
				</div>
			)
		})
		
		return (
			<div className='coffees__wrapper'>
				{coffeeItems}
			</div>
		)
	}
	
	const elements = useMemo(() => {
		return SetContentList(() => renderItems(filteredCoffees), process, filteredCoffees)
	}, [process, filteredCoffees])
	
	return (
		<section className='coffees'>
			<div className='coffees__container'>
				{elements}
			</div>
		</section>
	)
}

export default CoffeeList