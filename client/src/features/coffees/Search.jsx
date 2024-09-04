﻿import './styles/search.scss'
import useSearch from './use-search'

const Search = () => {
	const [search, handleClick] = useSearch()
	
	return (
		<form className='searchForm'>
			<label htmlFor='search' className='searchForm__label'>Lookiing for</label>
			<input
				className='searchForm__input'
				type='text'
				id='search'
				name='search'
				placeholder='start typing here...'
				autoComplete='off'
				value={search}
				onChange={handleClick}
			/>
		</form>
	)
}

export default Search