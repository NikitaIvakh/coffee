import './styles/filters.scss'
import useFilter from './use-filter'

const Filter = () => {
	const [filter, handleClick] = useFilter()
	
	const buttonsData = [
		{ name: '', label: 'All' },
		{ name: 'Brazil', label: 'Brazil' },
		{ name: 'Kenya', label: 'Kenya' },
		{ name: 'Columbia', label: 'Columbia' }
	]
	
	const isActive = (name) => {
		if (name === '') return filter === 'All'
		return filter === name
	}
	
	const buttons = buttonsData.map(({ name, label }) => {
		const active = isActive(name)
		const clazz = active ? 'btn-light' : 'btn-outline-light'
		
		return (
			<button
				className={`btn btn__filter ${clazz}`}
				type='button'
				key={name}
				onClick={() => handleClick(name)}
			>
				{label}
			</button>
		)
	})
	
	return (
		<form className='filter'>
			<label className='filter__label'>Or filter</label>
			{buttons}
		</form>
	)
}

export default Filter
