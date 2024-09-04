import { useEffect, useMemo, useState } from 'react'
import useHttp from '../../hooks/http.hook'
import './coffeeList.scss'
import SetContentList from '../../utils/SetContentList'

const CoffeeList = () => {
	const [coffees, setCoffees] = useState([])
	const { request, process, setProcess } = useHttp()
	
	useEffect(() => {
		onUpdateCoffees()
	}, [])
	
	const onUpdateCoffees = () => {
		request('https://localhost:8081/api/coffee/GetCoffeeList')
			.then(onCoffeesLoaded)
			.then(() => setProcess('confirmed'))
			.catch(res => console.log(res))
	}
	
	const onCoffeesLoaded = (data) => {
		setCoffees(coffees => [...coffees, ...data.value])
	}
	
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
		return SetContentList(() => renderItems(coffees), process, coffees)
	}, [process, coffees])
	
	return (
		<section className='coffees'>
			<div className='coffees__container'>
				{elements}
			</div>
		</section>
	)
}

export default CoffeeList