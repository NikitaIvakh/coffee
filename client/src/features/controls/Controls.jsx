import { useEffect, useMemo, useState } from 'react'
import useHttp from '../../hooks/http.hook'
import CoffeeList from '../../components/CoffeeList/CoffeeList'
import Filter from './Filters'
import Search from './Search'
import './styles/controls.scss'
import { useSelector } from 'react-redux'
import { selectSearch, selectFilters } from './controls-selectors'

const Controls = () => {
	const searchText = useSelector(selectSearch)
	const filter = useSelector(selectFilters)
	const [coffees, setCoffees] = useState([])
	const { request, process, setProcess } = useHttp()
	
	useEffect(() => {
		onUpdateCoffees()
	}, [])
	
	const onUpdateCoffees = () => {
		request('https://localhost:8081/api/coffee/GetCoffeeList')
			.then(onCoffeesLoaded)
			.then(() => setProcess('confirmed'))
			.catch(res => console.log(res))
	}
	
	const onCoffeesLoaded = (data) => {
		setCoffees(data.value)
	}
	
	const filteredCoffees = useMemo(() => {
		return coffees
			.filter(coffee => coffee.name.toLowerCase().includes(searchText.toLowerCase()))
			.filter(coffee => filter === 'All' || coffee['coffeeType'].toLowerCase() === filter.toLowerCase())
	}, [searchText, filter, coffees])
	
	return (
		<>
			<div className='controls__wrapper'>
				<Search />
				<Filter />
			</div>
			<CoffeeList process={process} filteredCoffees={filteredCoffees} />
		</>
	)
}

export default Controls
