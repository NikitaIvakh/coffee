import { useEffect, useMemo, useState } from 'react'
import useHttp from '../../hooks/http.hook'
import './best.scss'
import SetContentList from '../utils/SetContentList'

const Best = () => {
	const [coffees, setCoffees] = useState([])
	const { request, process, setProcess } = useHttp()
	
	useEffect(() => {
		onUpdateCoffees()
	}, [])
	
	const onUpdateCoffees = () => {
		request('https://localhost:5001/api/coffee/GetCoffeeList?Limit=3')
			.then(onCharLoaded)
			.then(() => setProcess('confirmed'))
			.catch(res => console.log(res))
	}
	
	const onCharLoaded = (data) => {
		setCoffees(coffees => [...coffees, ...data.value])
	}
	
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
		return SetContentList(() => renderItems(coffees), process, coffees)
	}, [process])
	
	return (
		<section className='our-best'>
			<h2 className='our-best__header'>Our best</h2>
			{elements}
		</section>
	)
}

export default Best