import { useMemo } from 'react'
import { CSSTransition, TransitionGroup } from 'react-transition-group'
import type { CoffeeItem } from 'types'
import SetContentList from 'utils/SetContentList'
import { useBase } from './use-base'
import './bestList.scss'

const BestList = () => {
	const [coffees, isAuthUser, { status }] = useBase()
	
	const renderItems = (coffees: CoffeeItem[]) => {
		const coffeeItems = coffees.map((coffee, i) => {
			const duration = 300
			const { name, price, imageUrl } = coffee
			
			return (
				<CSSTransition key={i} timeout={duration} classNames='coffee__item' unmountOnExit>
					<div key={i} className='our-best__list-item'>
						<img className='our-best__image' src={imageUrl} alt={name} />
						<div className='our-best__title'>{name}</div>
						<div className='our-best__price'>{price.toFixed(2)}$</div>
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
			{!isAuthUser ? (
					<>
						<h2 className='our-best__header-restricted'>Our best</h2>
						<p className='our-best__not-auth'>
							<span className='our-best__not-auth-title'>Access Restricted</span>
							<span className='our-best__not-auth-message'>
                Please log in to view our exclusive collection of top picks.
                We can't wait to share them with you!
					</span>
						</p>
					</>
				
				) :
				(
					<>
						<h2 className='our-best__header'>Our best</h2>
						{elements}
					</>)}
		</section>
	)
}

export default BestList
