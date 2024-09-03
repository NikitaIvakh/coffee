import './search.scss'

const Search = () => {
	return (
		<form className='searchForm'>
			<label htmlFor='search' className='searchForm__label'>Lookiing for</label>
			<input className='searchForm__input' type='text' id='search' name='search' placeholder='start typing here...' autoComplete="off"/>
		</form>
	)
}

export default Search