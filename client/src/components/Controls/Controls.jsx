﻿import { useEffect, useMemo, useState } from 'react'
import useHttp from '../../hooks/http.hook'
import CoffeeList from '../CoffeeList/CoffeeList'
import Filter from '../Filters/Filters'
import Search from '../Search/Search'
import './controls.scss'

const Controls = () => {
	const [coffees, setCoffees] = useState([])
	const [filter, setFilter] = useState('')
	const [searchText, setSearchText] = useState('')
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
		setCoffees(coffees => [...coffees, ...data.value])
	}
	
	const filteredCoffees = useMemo(() => {
		return coffees
			.filter(coffee => coffee.name.toLowerCase().includes(searchText.toLowerCase()))
			.filter(coffee => filter === '' || coffee['coffeeType'].toLowerCase() === filter.toLowerCase())
	}, [searchText, filter, coffees])
	
	return (
		<>
			<div className='controls__wrapper'>
				<Search setSearchText={setSearchText} />
				<Filter setFilter={setFilter} filteredCoffees={filteredCoffees} />
			</div>
			<CoffeeList process={process} filteredCoffees={filteredCoffees} />
		</>
	)
}

export default Controls