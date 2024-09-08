import { useMemo, useState } from 'react'
import { Link } from 'react-router-dom'
import { TransitionGroup, CSSTransition } from 'react-transition-group'
import useAdmin from '../admin/use-admin'
import ModalWindow from '../modal/ModalWindow'
import type { CoffeeItem } from '../../types'
import SetContentList from '../../utils/SetContentList'
import useCoffeesModal from '../modal/use-coffeesModal'
import useCoffees from './use-coffees'
import './coffeeList.scss'

interface CoffeeListProps {
	path: string;
	showButtons?: boolean;
}

const CoffeeList = (props: CoffeeListProps) => {
	const { path, showButtons } = props
	const [coffeeIsOpen, coffeeOpenModalWindow, coffeeCloseModalWindow] = useCoffeesModal()
	const [, , handleDeleteCoffee] = useAdmin()
	const [selectedCoffee, setSelectedCoffee] = useState<CoffeeItem | null>(null)
	const [coffees, pages, currentPage, isPending, { status }, handleClick] = useCoffees()
	
	const handleUpdateClick = (coffee: CoffeeItem) => {
		setSelectedCoffee(coffee)
		coffeeOpenModalWindow()
	}
	
	const handleDeleteClick = (coffee: CoffeeItem) => {
		handleDeleteCoffee(coffee.id)
	}
	
	const renderItems = (coffees: CoffeeItem[]) => {
		const coffeeItems = coffees.map((coffee) => {
			const { id, imageUrl, name, coffeeType, price } = coffee
			return (
				<CSSTransition timeout={500} key={id} className='coffees-item animate'>
					<div>
						<Link to={`/${path}/${id}`}>
							<img src={imageUrl} alt={name} />
							<div className='coffees-item__title'>{name}</div>
							<div className='coffees-item__sort'>{coffeeType}</div>
							<div className='coffees-item__price'>{price}$</div>
						</Link>
						{showButtons && (
							<div className='buttons__wrapper'>
								<button onClick={() => handleUpdateClick(coffee)} className='btn btn__filter'>Update</button>
								<button onClick={() => handleDeleteClick(coffee)} className='btn btn__filter'>Delete</button>
							</div>
						)}
					</div>
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
				{coffeeIsOpen && (
					<ModalWindow title='Update Coffee' isVisible={coffeeIsOpen} onClose={coffeeCloseModalWindow} coffee={selectedCoffee} />
				)}
				<TransitionGroup className='pages'>
					{pages.map((page) => (
						<CSSTransition
							key={page}
							timeout={300}
							classNames='animate'
						>
							<span
								className={currentPage === page ? 'current-page' : 'page'}
								onClick={() => handleClick(page)}
							>{page}
							</span>
						</CSSTransition>
					))}
				</TransitionGroup>
			</div>
		</section>
	)
}

export default CoffeeList
