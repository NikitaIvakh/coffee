import './coffeePromo.scss'

const CoffeePromo = (props) => {
	const { title } = props
	
	return (
		<div className='coffeePromo'>
			<h2 className='coffeePromo__title'>{title}</h2>
		</div>
	)
}

export default CoffeePromo