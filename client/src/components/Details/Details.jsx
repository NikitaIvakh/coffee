import { Fragment, useEffect, useMemo, useState } from 'react'
import { Link, useParams } from 'react-router-dom'
import useHttp from '../../hooks/http.hook'
import CoffeeImg from '../../resources/img/coffee/coffee_big.jpg'
import CoffeeBeansBlack from '../../resources/img/icons/main_beans_dark.svg'
import './details.scss'
import SetContentList from '../../utils/SetContentList'

const Details = () => {
	const { id } = useParams()
	const [coffees, setCoffee] = useState([])
	const { request, process, setProcess } = useHttp()
	
	useEffect(() => {
		onUpdateCoffee()
	}, [id])
	
	const onUpdateCoffee = () => {
		request(`https://localhost:8081/api/coffee/GetCoffee/${id}`)
			.then(onCoffeeLoaded)
			.then(() => setProcess('confirmed'))
	}
	
	const onCoffeeLoaded = (data) => {
		setCoffee(coffees => [...coffees, data.value])
	}
	
	const renderItems = (coffees) => {
		const coffeeItem = coffees.map((item, i) => {
			const { name, coffeeType, description, price } = item
			
			return (
				<Fragment key={i}>
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
		})
		
		return (
			<div className='details__wrapper'>
				{coffeeItem}
			</div>
		)
	}
	
	const elements = useMemo(() => {
		return SetContentList(() => renderItems(coffees), process, coffees)
	}, [id, process, coffees])
	
	
	return (
		<div className='details'>
			<div className='details__wrapper'>
				{elements}
			</div>
		</div>
	)
}

export default Details