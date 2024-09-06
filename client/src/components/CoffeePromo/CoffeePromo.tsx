import './coffeePromo.scss'

interface CoffeePromoProps {
	title: string
}

const CoffeePromo = (props: CoffeePromoProps) => {
	const { title } = props
	
	return (
		<div className='coffeePromo'>
			<h2 className='coffeePromo__title'>{title}</h2>
		</div>
	)
}

export default CoffeePromo