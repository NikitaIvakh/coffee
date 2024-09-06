import './styles/search.scss'
import useSearch from './use-search'

const Search = () => {
	const [search, isPending, handleClick] = useSearch()
	
	return (
		<form className='searchForm' style={{opacity: isPending ? 0.7 : 1}}>
			<label htmlFor='search' className='searchForm__label'>Lookiing for</label>
			<input
				className='searchForm__input'
				type='search'
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