﻿import { useMemo } from 'react'
import { Link } from 'react-router-dom'
import { CSSTransition, TransitionGroup } from 'react-transition-group'
import SetContentList from '../../utils/SetContentList'
import { useBase } from './use-base'
import './bestList.scss'

const BestList = () => {
	const [coffees, { status }] = useBase()
	
	const renderItems = (coffees) => {
		const coffeeItems = coffees.map((coffee, i) => {
			const duration = 300
			const { name, price, imageUrl } = coffee
			
			return (
				<CSSTransition key={i} timeout={duration} classNames='coffee__item' unmountOnExit>
					<div key={i} className='our-best__list-item'>
						<Link to={(`/OurCoffee/${coffee.id}`)}>
							<img className='our-best__image' src={imageUrl} alt={name} />
							<div className='our-best__title'>{name}</div>
							<div className='our-best__price'>{price.toFixed(2)}$</div>
						</Link>
					</div>
				</CSSTransition>
			)
		})
		
		return (
			<div className='our-best__wrapper'>
				<TransitionGroup component={null}>{coffeeItems}</TransitionGroup>
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
