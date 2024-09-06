import './coffeeList.scss'
import { useMemo } from 'react'
import { Link } from 'react-router-dom'
import { TransitionGroup, CSSTransition } from 'react-transition-group'
import type { CoffeeItem } from '../../types'
import SetContentList from '../../utils/SetContentList'
import useCoffees from './use-coffees'

interface CoffeeListProps {
	path: string
}

const CoffeeList = (props: CoffeeListProps) => {
	const { path } = props
	const [coffees, pages, currentPage, isPending, { status }, handleClick] = useCoffees()
	
	const renderItems = (coffees: CoffeeItem[]) => {
		const coffeeItems = coffees.map((coffee) => {
			const { id, imageUrl, name, coffeeType, price } = coffee
			return (
				<CSSTransition timeout={500} key={id} className='coffees-item animate'>
					<Link to={`/${path}/${id}`}>
						<img src={imageUrl} alt={name} />
						<div className='coffees-item__title'>{name}</div>
						<div className='coffees-item__sort'>{coffeeType}</div>
						<div className='coffees-item__price'>{price}$</div>
					</Link>
				</CSSTransition>
			)
		})
		
		return (
			<div className='coffees__wrapper'>
				<TransitionGroup component={null}>{coffeeItems}</TransitionGroup>
			</div>
		)
	}
	
	const elements = useMemo(() => {
		return SetContentList(() => renderItems(coffees), status, coffees)
	}, [status, coffees, currentPage])
	
	return (
		<section className='coffees' style={{ opacity: isPending ? 0.7 : 1 }}>
			<div className='coffees__container'>
				{elements}
				<TransitionGroup className='pages'>
					{pages.map((page, i) => (
						<CSSTransition
							key={page}
							timeout={300}
							classNames='animate'
						>
							<span
								className={currentPage === page ? 'current-page' : 'page'}
								onClick={() => handleClick(page)}
							>
								{page}
							</span>
						</CSSTransition>
					))}
				</TransitionGroup>
			</div>
		</section>
	)
}

export default CoffeeList
