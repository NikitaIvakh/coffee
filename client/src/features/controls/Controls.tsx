import CoffeeList from '../coffees/CoffeeList'
import Filter from './Filters'
import Search from './Search'
import './styles/controls.scss'

interface ControlProps {
	path: string
	showButtons?: boolean
}

const Controls = (props: ControlProps) => {
	const { path, showButtons } = props
	
	return (
		<>
			<div className='controls'>
				<Search />
				<Filter />
			</div>
			<CoffeeList path={path} showButtons={showButtons}/>
		</>
	)
}

export default Controls
