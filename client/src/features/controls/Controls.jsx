import CoffeeList from '../coffees/CoffeeList'
import Filter from './Filters'
import Search from './Search'
import './styles/controls.scss'

const Controls = (props) => {
	const { path } = props
	
	return (
		<>
			<div className='controls'>
				<Search />
				<Filter />
			</div>
			<CoffeeList path={path} />
		</>
	)
}

export default Controls
