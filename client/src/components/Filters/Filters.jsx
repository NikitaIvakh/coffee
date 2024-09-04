import './filters.scss'

const Filter = ({ filteredCoffees, setFilter }) => {
	const buttonsData = [
		{ name: '', label: 'All' },
		{ name: 'Brazil', label: 'Brazil' },
		{ name: 'Kenya', label: 'Kenya' },
		{ name: 'Columbia', label: 'Columbia' }
	]
	
	const isActive = (name) => {
		if (name === '') return filteredCoffees.length === 0
		return filteredCoffees.every(coffee => coffee['coffeeType'] === name)
	}
	
	const buttons = buttonsData.map(({ name, label }) => {
		const active = isActive(name)
		const clazz = active ? 'btn-light' : 'btn-outline-light'
		
		return (
			<button
				className={`btn btn__filter ${clazz}`}
				type='button'
				key={name}
				onClick={() => setFilter(name)}
			>{label}</button>
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
