import './search.scss'
import { useState } from 'react'

const Search = ({ setSearchText }) => {
	const [text, setText] = useState('')
	
	const onChange = (event) => {
		const value = event.target.value
		setText(value)
		setSearchText(value)
	}
	
	return (
		<form className='searchForm'>
			<label htmlFor='search' className='searchForm__label'>Lookiing for</label>
			<input
				className='searchForm__input'
				type='text' id='search'
				name='search'
				placeholder='start typing here...'
				autoComplete='off'
				value={text}
				onChange={onChange}
			/>
		</form>
	)
}

export default Search