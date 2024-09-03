import Filter from '../Filters/Filters'
import Search from '../Search/Search'
import "./controls.scss"

const Controls = () => {
	return (
		<div className='controls__wrapper'>
			<Search />
			<Filter />
		</div>
	)
}

export default Controls