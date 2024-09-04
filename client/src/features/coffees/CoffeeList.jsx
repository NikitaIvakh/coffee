import './coffeeList.scss'
import { useMemo } from 'react'
import { useNavigate } from 'react-router-dom'
import SetContentList from '../../utils/SetContentList'
import useCoffees from './use-coffees'

const CoffeeList = () => {
	const navigate = useNavigate()
	const [coffees, { status }] = useCoffees()
	
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
		return SetContentList(() => renderItems(coffees), status, coffees)
	}, [status, coffees])
	
	return (
		<section className='coffees'>
			<div className='coffees__container'>
				{elements}
			</div>
		</section>
	)
}

export default CoffeeList