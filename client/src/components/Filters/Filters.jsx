import "./filter.scss"

const Filter = () => {
	return (
		<form className='filter'>
			<label className='filter__label'>Or filter</label>
			<button className='btn btn__filter'>Brazil</button>
			<button className='btn btn__filter'>Kenya</button>
			<button className='btn btn__filter'>Columbia</button>
		</form>
	)
}

export default Filter