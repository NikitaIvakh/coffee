import CoffeeList from '../coffees/CoffeeList'
import Filter from './Filters'
import Search from './Search'
import './styles/controls.scss'

const Controls = () => {
	return (
		<>
			<div className='controls__wrapper'>
				<Search />
				<Filter />
			</div>
			<CoffeeList />
		</>
	)
}

export default Controls
